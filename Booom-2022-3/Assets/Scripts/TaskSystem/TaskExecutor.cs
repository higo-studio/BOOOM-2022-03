/*
 * @Author: chunibyou
 * @Date: 2022-04-07 20:10:29
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-18 20:52:26
 * @Description: 用于实现任务的执行者功能(暂时单一功能)
 */

using System.Collections.Generic;
using UnityEngine;
using TaskSystem;

public class TaskExecutor : MonoBehaviour, ITaskFinishHandle
{
    public float score { get; private set; } = 0.0f;

    public void OnTaskFinish(Task task)
    {
        score += task.reward;
        Debug.Log($"任务 : “{task.name}” 完成，奖励分数 {task.reward}");
        TaskUIManager.Instance.UpdateScore(score);
    }

    public void OnTaskFail(Task task)
    {
        score -= task.penal;
        if(score < 0)
            score = 0;
        Debug.Log($"任务 : “{task.name}” 失败，惩罚分数 {task.penal}");
        TaskUIManager.Instance.UpdateScore(score);
    }
}
