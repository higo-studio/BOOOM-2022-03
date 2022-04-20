/*
 * @Author: chunibyou
 * @Date: 2022-03-31 22:14:11
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-20 23:09:35
 * @Description: 任务系统组件
 */
using UnityEngine;

namespace TaskSystem
{

    // 任务类型
    public enum TaskType
    {
        PLACED = 1,         // 放置(送到目的地)
    }

    // 任务状态
    public enum TaskState
    {
        DOESNT_EXSIT = 0,
        WAITING = 1,
        ACCEPT = 2,
        COMPLETE = 3,
        FAIL = 4,
    }

    // 任务 只做了单一目标和资源
    [System.Serializable]
    public class Task
    {
        public TaskType type { get; }

        [SerializeField]
        private TaskState state = TaskState.WAITING;

        public string name;

        public float reward { get; private set; } 

        public float penal { get; private set; } 

        [SerializeField]
        public float limitTime { get; private set; }

        [SerializeField]
        public float time { get; private set; }

        // 持有目标资源
        [SerializeField]
        private float source;

        // 完成任务所需
        public float targetSource { get; }

        private Task(string _name, float _limitTime)
        {
            type = TaskType.PLACED;
            name = _name;
            time = 0;
            limitTime = _limitTime;
            source = 0;
            targetSource = 1;
            reward = 0;
            penal = 0;
        } 

        private Task(string _name, float _limitTime, float _reward, float _penal)
        {
            type = TaskType.PLACED;
            name = _name;
            time = 0;
            limitTime = _limitTime;
            source = 0;
            targetSource = 1;
            reward = _reward;
            penal = _penal;
        } 

        private Task(string _name, float _targetSource, float _limitTime)
        {
            type = TaskType.PLACED;
            name = _name;
            time = 0;
            limitTime = _limitTime;
            source = 0;
            targetSource = _targetSource;
            reward = 0;
            penal = 0;
        }

        private Task(string _name, float _targetSource, float _limitTime, float _reward, float _penal)
        {
            type = TaskType.PLACED;
            name = _name;
            time = 0;
            limitTime = _limitTime;
            source = 0;
            targetSource = _targetSource;
            reward = _reward;
            penal = _penal;
        } 

        public static Task CreateTask(string name, float _limitTime)
        {
            Task task = new Task(name, _limitTime);
            return task;
        }

        public static Task CreateTask(string name, float _limitTime, float _reward, float _penal)
        {
            Task task = new Task(name, _limitTime, _reward, _penal);
            return task;
        }

        public float GetSource()
        {
            return source;
        }

        public void AddSource(float adding)
        {
            source += adding;
        }

        public TaskState GetState()
        {
            return state;
        }

        public bool AcceptTask()
        {
            if(state == TaskState.WAITING)
            {
                state = TaskState.ACCEPT;
                return true;
            }
            return false;
        }

        public bool CompleteTask()
        {
            if(state == TaskState.ACCEPT)
            {
                state = TaskState.COMPLETE;
                return true;
            }
            return false;
        }

        public bool FailTask()
        {
            if(state == TaskState.ACCEPT)
            {
                state = TaskState.FAIL;
                return true;
            }
            return false;
        }

        public bool CheckComplete()
        {
            return source >= targetSource;
        }

        public bool CheckFail()
        {
            return time >= limitTime;
        }

        public void UpdateTime(float deltaTime)
        {
            time += deltaTime;
        }
    }

    public interface ITaskFinishHandle
    {
        public void OnTaskFinish(Task task);
        public void OnTaskFail(Task task);
    }
}

