/*
 * @Author: chunibyou
 * @Date: 2022-04-18 17:13:07
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-30 16:42:34
 * @Description: 单例，用于把任务信息给到UI
 */

using TaskSystem;

class TaskUIManager
{
    public static TaskUIManager Instance { get { return instance; } }

    private static TaskUIManager instance = new TaskUIManager();

    private TaskUIManager() { }

    private ITaskUITimer curr;

    // 获取到TaskTarget的引用
    public void RegisterCurr(ITaskUITimer target)
    {
        if(target == curr)
            return;
        curr = target;
    }

    // 船只离开检查点时注销引用
    public void CancelCurr(ITaskUITimer target)
    {
        if(target != curr)
            return;
        curr = null;
    }


    // 获取当前检查点，可能为null
    public ITaskUITimer GetTaskTimer()
    {
        return curr;
    }

    public float score { get; private set; }

    public void UpdateScore(float _score)
    {
        score = _score;
    }

    

}
