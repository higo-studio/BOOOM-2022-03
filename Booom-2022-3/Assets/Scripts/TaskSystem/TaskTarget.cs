/*
 * @Author: chunibyou
 * @Date: 2022-04-07 11:01:40
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-30 18:52:44
 * @Description: 挂载在终点
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaskSystem;

[RequireComponent(typeof(Collider))]
public class TaskTarget : MonoBehaviour, ITaskUITimer, ITalkable
{

    [Header("需在区域内停靠时间")]
    public float holdingTime = 2.0f;

    [Header("对话的JSON")]
    public int wordsIndex = -1;

    private bool talked = false;

    bool talking = false;

    [SerializeField]
    private bool isShipStay = false;

    private bool done = false;

    public List<string> taskNames;

    private TaskManagerMono taskManagerMono;

    private float stayTime = 0;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && IsTargetNecessary())
        {
            isShipStay = true;
            TaskUIManager.Instance.RegisterCurr(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            stayTime = 0;
            isShipStay = false;
            done = false;
            TaskUIManager.Instance.CancelCurr(this);
        }
    }

    public void SetTaskManagerMono(TaskManagerMono mono)
    {
        taskManagerMono = mono;
    }

    public float GetCurrStayTime()
    {
        return stayTime;
    }

    public bool IsDone()
    {
        return done;
    }

    // 判断是否有任务点对应的任务正在执行
    private bool IsTargetNecessary()
    {
        bool necessary = false;
        foreach (var name in taskNames)
        {
            necessary = necessary || taskManagerMono.manager.IsTaskWorking(name);
        }
        return necessary;
    }

    private void FixedUpdate()
    {
        if (!isShipStay)
        {
            return;
        }
        stayTime += Time.fixedDeltaTime;
        if (stayTime >= holdingTime && !done)
        {
            foreach (var name in taskNames)
            {
                taskManagerMono.AddTaskSource(name);
            }
            stayTime = 0;
            done = true;
            OnTalkStart();
        }
    }

    public float GetCurrTime() { return stayTime; }

    public float GetHoldingTime() { return holdingTime; }

    // 强制对话正常结束
     public bool InArea()
    {
        return true;
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
