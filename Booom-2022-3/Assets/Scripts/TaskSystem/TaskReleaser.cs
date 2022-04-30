/*
 * @Author: chunibyou
 * @Date: 2022-04-07 11:20:08
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-30 16:49:31
 * @Description: 挂载在NPC上
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TaskReleaser : MonoBehaviour, ITaskUITimer
{
    public string taskName;

    [SerializeField]
    private bool isShipStay = false;

    [Header("需在区域内停靠时间")]
    public float holdingTime = 2.0f;

    private float time = 0;

    private bool done = false;

    private TaskManagerMono taskManagerMono;

    public void SetTaskManagerMono(TaskManagerMono mono)
    {
        taskManagerMono = mono;
    }

    private void FixedUpdate()
    {
        if (!isShipStay)
            return;
        time += Time.fixedDeltaTime;
        if (time >= holdingTime)
        {
            done = true;
            if (Input.GetButtonDown("Accept") && !taskManagerMono.GetTaskManager().IsTaskWorking(taskName))
            {
                taskManagerMono.ReleaseTask(taskName);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isShipStay = true;
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
            TaskUIManager.Instance.CancelCurr(this);
        }
    }

    public float GetCurrTime() { return time; }

    public float GetHoldingTime() { return holdingTime; }

    public bool IsDone() { return done; }
}
