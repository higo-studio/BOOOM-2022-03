using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaskSystem;

[System.Serializable]
public class TaskInformation
{
    public string name;
    public float limitTime;
    public float reward;
    public float penal;
    public GameObject releaser;
    public GameObject target;
}

//[CreateAssetMenu(menuName = "Create NormalTasks")]
public class NormalTasks : MonoBehaviour
{
    public List<TaskInformation> taskList;

    private void Start() {
        foreach(TaskInformation information in taskList)
        {
            information.releaser.GetComponent<TaskReleaser>().taskName = information.name;
            information.target.GetComponent<TaskTarget>().taskNames.Add(information.name);
        }
    }

    
}
