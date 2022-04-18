/*
 * @Author: chunibyou
 * @Date: 2022-04-18 17:13:07
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-18 20:45:58
 * @Description: 单例，用于把任务信息给到UI
 */

using TaskSystem;

class TaskUIManager
{
    public static TaskUIManager Instance { get { return instance; } }

    private static TaskUIManager instance = new TaskUIManager();

    private TaskUIManager() { }

    private TaskTarget currTarget;

    // 获取到TaskTarget的引用
    public void RegisterCurrTarget(TaskTarget target)
    {
        if(target == currTarget)
            return;
        currTarget = target;
    }

    // 船只离开检查点时注销引用
    public void CancelCurrTarget(TaskTarget target)
    {
        if(target != currTarget)
            return;
        currTarget = null;
    }

    // 获取当前检查点，可能为null
    public TaskTarget GetCurrTarget()
    {
        return currTarget;
    }

    public float score { get; private set; }

    public void UpdateScore(float _score)
    {
        score = _score;
    }

}
