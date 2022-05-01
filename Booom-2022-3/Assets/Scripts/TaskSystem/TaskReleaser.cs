/*
 * @Author: chunibyou
 * @Date: 2022-04-07 11:20:08
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-05-01 16:32:38
 * @Description: 挂载在NPC上
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TaskReleaser : MonoBehaviour, ITaskUITimer, ITalkable
{
    public string taskName;

    [SerializeField]
    private bool isShipStay = false;

    [Header("需在区域内停靠时间")]
    public float holdingTime = 2.0f;

    private float time = 0;

    private bool done = false;

    [Header("对话的JSON")]
    public int wordsIndex = -1;

    private bool talked = false;

    private TaskManagerMono taskManagerMono;

    public void SetTaskManagerMono(TaskManagerMono mono)
    {
        taskManagerMono = mono;
    }

    bool talking = false;
    public bool Talking => talking;
    private void FixedUpdate()
    {
        if (!isShipStay)
            return;

        if (!taskManagerMono.GetTaskManager().IsTaskWorking(taskName))
        {
            if (Input.GetButton("Accept"))
            {
                time += Time.fixedDeltaTime;
            }
            else
            {
                time -= Time.fixedDeltaTime;
                if (time < 0)
                {
                    time = 0;
                }
            }
            if (time >= holdingTime)
            {
                if (!talking)
                {
                    OnTalkStart();

                }
            }
            if (talked)
            {
                taskManagerMono.ReleaseTask(taskName);
            }
        }
        //time += Time.fixedDeltaTime;
        //if (time >= holdingTime)
        //{
        //done = true;
        //if (Input.GetButtonDown("Accept") && !taskManagerMono.GetTaskManager().IsTaskWorking(taskName))
        //{
        //    if(talked)
        //    {
        //        taskManagerMono.ReleaseTask(taskName);
        //    }
        //    else if(!talking)
        //    {
        //        OnTalkStart();
        //    }

        //}
        //}

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isShipStay = true;
            talked = false;
            TaskUIManager.Instance.RegisterCurr(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isShipStay = false;
            time = 0;
            done = false;
            talked = false;
            TaskUIManager.Instance.CancelCurr(this);
        }
    }

    public float GetCurrTime() { return time; }

    public float GetHoldingTime() { return holdingTime; }

    public bool IsDone() { return done; }

    public bool InArea()
    {
        return isShipStay;
    }

    public void OnTalkStart()
    {
        talking = true;
        taskManagerMono.dialogue.Speak(this, wordsIndex);
    }

    public void OnTalkEnd(bool normalEnding)
    {
        talked = normalEnding;
        talking = false;
    }
}
