/*
 * @Author: chunibyou
 * @Date: 2022-04-07 11:20:08
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-07 20:03:54
 * @Description: 挂载在NPC上
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TaskReleaser : MonoBehaviour
{
    public string taskName;

    private TaskManagerMono taskManagerMono;

    public void SetTaskManagerMono(TaskManagerMono mono)
    {
        taskManagerMono = mono;
    }

    private void OnTriggerEnter(Collider other) {
        taskManagerMono.ReleaseTask(taskName);
    }
}
