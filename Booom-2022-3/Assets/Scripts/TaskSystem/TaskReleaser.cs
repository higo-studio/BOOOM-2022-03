/*
 * @Author: chunibyou
 * @Date: 2022-04-07 11:20:08
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-05-01 17:34:21
 * @Description: 挂载在NPC上
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaskSystem;

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

    private void Awake() {
        perPosition = transform.position;
    }

    public void SetTaskManagerMono(TaskManagerMono mono)
    {
        taskManagerMono = mono;
    }

    bool talking = false;
    public bool Talking => talking;

    private Vector3 perPosition;
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
                    TaskUIManager.Instance.CancelCurr(this);
                }
            }
            if (talked)
            {
                taskManagerMono.ReleaseTask(taskName);
            }
        }
        
        TaskState state = taskManagerMono.GetTaskManager().GetTaskState(taskName);
        if(state == TaskState.ACCEPT || state == TaskState.COMPLETE)
        {
            //隐藏
            perPosition = transform.position;
            transform.position = new Vector3(perPosition.x, 500, perPosition.z);
        }
        else if(state == TaskState.FAIL || state == TaskState.WAITING)
        {
            //显示
            transform.position = perPosition;
        }
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
