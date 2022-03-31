/*
 * @Author: chunibyou
 * @Date: 2022-03-31 22:13:33
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-03-31 23:42:34
 * @Description: 任务系统
 */


using System.Collections;
using System.Collections.Generic;

namespace TaskSystem
{
    public class TaskManager
    {
        private Dictionary<Task, TaskFinishHandle> taskMap = new Dictionary<Task, TaskFinishHandle>();

        public void Update() {
            CheeckTaskFinished();    
        }

        // 轮询触发
        public void CheeckTaskFinished()
        {
            var keys = taskMap.Keys;
            foreach(var key in keys)
            {
                if(key.Check())
                {
                    taskMap[key].TaskFinish();
                }
            }
        }

        // 注册
        public void RegisterHandle(Task task, TaskFinishHandle handle)
        {
            taskMap.Add(task, handle);
        }

        // TODO: 把创建Task的接口在manager里实现，使用只需注册
    }

}