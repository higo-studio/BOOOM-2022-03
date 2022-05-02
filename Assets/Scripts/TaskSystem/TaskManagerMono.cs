/*
 * @Author: chunibyou
 * @Date: 2022-04-07 18:42:43
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-30 18:48:14
 * @Description: 保存Manger的mono，便于数据获取和流程控制
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaskSystem;

public class TaskManagerMono : MonoBehaviour
{

    public TaskManager manager = new TaskManager();

    public TaskExecutor executor;

    public NormalTasks normalTasks;

    public Dialogue dialogue;

    private void Awake()
    {
        if (normalTasks != null)
        {
            manager.CreateTaskByInformationList(normalTasks.taskList);
            if (executor != null)
            {
                foreach (var information in normalTasks.taskList)
                {
                    manager.RegisterHandle(information.name, executor);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 将mono的引用传递
        foreach (var information in normalTasks.taskList)
        {
            TaskReleaser releaser = information.releaser.GetComponent<TaskReleaser>();
            TaskTarget target = information.target.GetComponent<TaskTarget>();
            releaser.SetTaskManagerMono(this);
            target.SetTaskManagerMono(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        manager.Update(Time.deltaTime);
    }

    // 给TaskTarget调用，增加source以进入Complete流程
    public bool AddTaskSource(string taskName)
    {
        if (!manager.IsTaskWorking(taskName))
            return false;
        manager.AddSource(taskName);
        return true;
    }

    public bool ReleaseTask(string taskName)
    {
        return manager.AcceptTask(taskName);
    }

    public TaskManager GetTaskManager()
    {
        return manager;
    }
}
