/*
 * @Author: chunibyou
 * @Date: 2022-04-07 11:20:08
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-18 16:18:14
 * @Description: 挂载在NPC上
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TaskReleaser : MonoBehaviour
{
    public string taskName;

    [SerializeField]
    private bool isShipStay = false;

    private TaskManagerMono taskManagerMono;

    public void SetTaskManagerMono(TaskManagerMono mono)
    {
        taskManagerMono = mono;
    }

    private void FixedUpdate() 
    {
        if(!isShipStay)
            return;
        if(Input.GetButtonDown("Accept") && !taskManagerMono.GetTaskManager().IsTaskWorking(taskName))
            taskManagerMono.ReleaseTask(taskName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isShipStay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isShipStay = false;
        }
    }
}
