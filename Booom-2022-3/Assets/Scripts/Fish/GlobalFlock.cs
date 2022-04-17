using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject goalPrefab;

    public int numFish = 10;//数量
    public static GameObject[] allFish;

    public Vector3 tankSize;//生成范围

    public bool randomGoal = true;//是否随机游走


    public static Vector3 goalPos = Vector3.zero;//目标点

    void Start()
    {
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-tankSize.x, tankSize.x),
                Random.Range(-tankSize.y, tankSize.y),
                Random.Range(-tankSize.z, tankSize.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, transform.position + pos, Quaternion.identity, this.transform);
            if (allFish[i].GetComponent<Flock>() == null)
            {
                allFish[i].AddComponent<Flock>();
            }
        }
    }

    void Update()
    {
        if (!randomGoal)
        {
            goalPos = goalPrefab.transform.position;
        }
        else
        {
            if (Random.Range(0, 10000) < 10)
            {
                goalPos = transform.position + new Vector3(Random.Range(-tankSize.x, tankSize.x),
                Random.Range(-tankSize.y, tankSize.y),
                Random.Range(-tankSize.z, tankSize.z));

                goalPrefab.transform.position = goalPos;
            }

        }
    }
}
