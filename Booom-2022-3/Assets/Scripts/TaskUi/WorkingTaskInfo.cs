/*
 * @Author: chunibyou
 * @Date: 2022-04-20 21:50:14
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-21 00:31:42
 * @Description: 生成UI信息列表
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TaskSystem;

public class WorkingTaskInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject taskInfoPrefab;

    [SerializeField]
    private TaskManagerMono taskManagerMono;

    [SerializeField]
    private int workingTaskCount = 0;

    private List<TextMeshProUGUI> taskTexts = new List<TextMeshProUGUI>();
    
    private List<Task> taskList;

    private TaskManager taskManager;

    private void Awake() 
    {
        if(taskInfoPrefab == null)
            Debug.LogError("WorkingTaskInfo : 缺少Prefab引用");  
        if(taskManagerMono == null)
            Debug.LogError("WorkingTaskInfo : 缺少TaskManagerMono引用");  
    }

    private void Start() 
    {
        taskManager = taskManagerMono.GetTaskManager();
    }

    // 对列表的更新
    private void FixedUpdate() 
    {
        taskList = taskManager.GetAcceptedTaskList();
        workingTaskCount = taskList.Count;
        if(workingTaskCount > taskTexts.Count)
        {
            int n = workingTaskCount - taskTexts.Count;
            for(int i = 0; i < n; i++)
            {
                GameObject textObj = Instantiate(taskInfoPrefab);
                textObj.transform.SetParent(transform, false);
                taskTexts.Add(textObj.GetComponent<TextMeshProUGUI>());
                // position
                Vector3 pos = textObj.transform.position;
                pos.y += (workingTaskCount - 1) * -textObj.GetComponent<RectTransform>().rect.height;
                textObj.transform.position = pos;
            }
        }
    }

    // 对任务的更新
    private void Update() 
    {
        for(int i = 0; i < workingTaskCount; i++)
        {
            if(!taskTexts[i].gameObject.activeInHierarchy)
                taskTexts[i].gameObject.SetActive(true);
            taskTexts[i].SetText($"{taskList[i].name} : 倒计时{(taskList[i].limitTime - taskList[i].time).ToString("0.0")}");
        }
        for(int i = workingTaskCount; i < taskTexts.Count; i++)
        {
            if(taskTexts[i].gameObject.activeInHierarchy)
                taskTexts[i].gameObject.SetActive(false);
        }
    }
}
