/*
 * @Author: chunibyou
 * @Date: 2022-03-31 22:13:33
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-18 20:37:34
 * @Description: 任务系统
 */


using System.Collections.Generic;
namespace TaskSystem
{
    public class TaskManager
    {
        private Dictionary<string, Task> taskMap = new Dictionary<string, Task>();
        private Dictionary<string, ITaskFinishHandle> handleMap = new Dictionary<string, ITaskFinishHandle>();

        // 根据TaskInformation的配置信息创建Task
        public void CreateTaskByInformationList(List<TaskInformation> list)
        {
            foreach(var information in list)
            {
                CreateTaskByInformation(information);
            }
        }

        // 根据TaskInformation的配置信息创建Task
        public void CreateTaskByInformation(TaskInformation information)
        {
            Task task = Task.CreateTask(information.name, information.limitTime, information.reward, information.penal);
            taskMap.Add(information.name, task);
        }

        public void Update(float deltaTime) {
            TaskUpdateTime(deltaTime);
            CheeckTaskFail();
            CheeckTaskFinished();
        }

        // 任务计时
        public void TaskUpdateTime(float deltaTime)
        {
            var tasks = new List<Task>(taskMap.Values);
            foreach(var task in tasks)
            {
                if(task.GetState() != TaskState.ACCEPT)
                    continue;
                task.UpdateTime(deltaTime);
            }
        }

        // 轮询失败
        public void CheeckTaskFail()
        {
            var tasks = new List<Task>(taskMap.Values);
            foreach(var task in tasks)
            {
                if(task.GetState() == TaskState.ACCEPT && task.CheckFail() && handleMap.ContainsKey(task.name))
                {
                    task.FailTask();
                    handleMap[task.name].OnTaskFail(task);
                }
            }
        }

        // 轮询触发
        public void CheeckTaskFinished()
        {
            var tasks = new List<Task>(taskMap.Values);
            foreach(var task in tasks)
            {
                if(task.GetState() == TaskState.ACCEPT && task.CheckComplete() && handleMap.ContainsKey(task.name))
                {
                    task.CompleteTask();
                    handleMap[task.name].OnTaskFinish(task);
                }
            }
        }

        // 注册
        public bool RegisterHandle(string taskName, ITaskFinishHandle handle)
        {
            if(!IsTaskExist(taskName))
                return false;
            handleMap.Add(taskName, handle);
            return true;
        }

        // 获取正在执行的任务集合
        public List<Task> GetAcceptedTaskList()
        {
            List<Task> list = new List<Task>();
            foreach(var task in taskMap.Values)
            {
                if(task.GetState() == TaskState.ACCEPT)
                    list.Add(task);
            }
            return list;
        }

        // 返回所有任务集合
        public List<Task> GetAllTasks()
        {
            return new List<Task>(taskMap.Values);
        }

        // 查询任务是否存在
        public bool IsTaskExist(string taskName)
        {
            return taskMap.ContainsKey(taskName);
        }

        // 获取任务状态
        public TaskState GetTaskState(string taskName)
        {
            if(!IsTaskExist(taskName))
                return TaskState.DOESNT_EXSIT;
            return taskMap[taskName].GetState();
        }

        // 查询任务是否正在进行
        public bool IsTaskWorking(string taskName)
        {
            return IsTaskExist(taskName) && GetTaskState(taskName) == TaskState.ACCEPT;
        }

        public void AddSource(string taskName)
        {
            if(!IsTaskExist(taskName))
                return;
            taskMap[taskName].AddSource(1.0f);
        }

        public void AddSource(string taskName, float source)
        {
            if(!IsTaskExist(taskName))
                return;
            taskMap[taskName].AddSource(source);
        }

        public bool AcceptTask(string taskName)
        {
            if(!IsTaskExist(taskName))
                return false;
            return taskMap[taskName].AcceptTask(); 
        }
    }

}