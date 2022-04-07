/*
 * @Author: chunibyou
 * @Date: 2022-04-07 20:10:29
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-07 20:14:29
 * @Description: 用于实现任务的执行者功能(暂时单一功能)
 */

using System.Collections.Generic;
using UnityEngine;
using TaskSystem;

public class TaskExecutor : MonoBehaviour, ITaskFinishHandle
{
    public void OnTaskFinish(Task task)
    {
        Debug.Log(task.name + "   搞掂！");
    }

    public void OnTaskFail(Task task)
    {
        Debug.Log(task.name + "   搞衰啦叉烧！");
    }
}
