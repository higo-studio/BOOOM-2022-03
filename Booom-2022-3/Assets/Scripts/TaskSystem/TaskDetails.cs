/*
 * @Author: chunibyou
 * @Date: 2022-03-31 22:14:11
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-03-31 23:38:45
 * @Description: 任务系统组件
 */

namespace TaskSystem
{

    // 任务类型
    public enum TaskType
    {
        PLACED = 1,         // 放置(送到目的地)
    }

    // 任务 只做了单一目标和资源
    public class Task
    {
        public TaskType type { get; }

        public string name { get; }

        // 持有目标资源
        private float source;

        // 完成任务所需
        public float targetSource { get; }

        private Task(string _name)
        {
            type = TaskType.PLACED;
            name = _name;
            source = 0;
            targetSource = 1;
        } 

        private Task(string _name, float _targetSource)
        {
            type = TaskType.PLACED;
            name = _name;
            source = 0;
            targetSource = _targetSource;
        } 

        public static Task CreateTask(string name)
        {
            Task task = new Task(name);
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

        public bool Check()
        {
            return source >= targetSource;
        }
    }

    public interface TaskFinishHandle
    {
        public void TaskFinish();
    }
}

