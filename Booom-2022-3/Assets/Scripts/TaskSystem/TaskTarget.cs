/*
 * @Author: chunibyou
 * @Date: 2022-04-07 11:01:40
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-18 16:18:46
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

    public List<string> taskNames;

    private TaskManagerMono taskManagerMono;

    private float stayTime = 0;

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

    public void SetTaskManagerMono(TaskManagerMono mono)
    {
        taskManagerMono = mono;
    }

    private void FixedUpdate()
    {
        if(!isShipStay)
        {
            return;
        }
        stayTime += Time.fixedDeltaTime;
        if (stayTime >= holdingTime)
        {
            foreach (var name in taskNames)
            {
                taskManagerMono.AddTaskSource(name);
            }
            stayTime = 0;
        }
    }

}
