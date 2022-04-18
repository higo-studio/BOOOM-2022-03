/*
 * @Author: chunibyou
 * @Date: 2022-04-07 11:01:40
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-18 18:39:40
 * @Description: 挂载在终点
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaskSystem;

[RequireComponent(typeof(Collider))]
public class TaskTarget : MonoBehaviour
{

    [Header("需在区域内停靠时间")]
    public float holdingTime = 2.0f;

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
            TaskUIManager.Instance.RegisterCurrTarget(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            stayTime = 0;
            isShipStay = false;
            done = false;
            TaskUIManager.Instance.CancelCurrTarget(this);
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

    public bool IsTargetDone()
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
        }
    }

}
