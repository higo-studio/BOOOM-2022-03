/*
 * @Author: chunibyou
 * @Date: 2022-04-07 11:01:40
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-07 19:23:39
 * @Description: 挂载在终点
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaskSystem;

[RequireComponent(typeof(Collider))]
public class TaskTarget : MonoBehaviour
{

    public List<string> taskNames;

    private TaskManagerMono taskManagerMono;

    private void OnTriggerEnter(Collider other)
    {
        foreach(var name in taskNames)
        {
            taskManagerMono.AddTaskSource(name);
        }
    }

    public void SetTaskManagerMono(TaskManagerMono mono)
    {
        taskManagerMono = mono;
    }

}
