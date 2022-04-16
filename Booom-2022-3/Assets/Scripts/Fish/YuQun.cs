using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class YuQun : MonoBehaviour
{
    public int fishNum;
    public GameObject fishPre;
    public float range;
    public string thisTag;
    public string foodTag;
    public string enemyTag;
    public float escapeDis;
    public float escapeWeight;
    public float predationDis;
    public float predationWeight;
    public float keepCreaterWeight;
    public float findFriendDis;
    public float toFriendsCenterWeight;
    public float keepFriendDis;
    public float keepFriendDisWeight;
    public float keepFriendSpeedWeight;
    public float randWeight;
    public float maxSpeed;
    public float maxTurnSpeed;
    public float maxAcceleration;
    public float minCD;
    public float maxCD;
    public List<Yu> yuqun;
    public Thread th1;
    public Thread th2;
    void Start()
    {
        yuqun = new List<Yu>();
        for (int i = 0; i < fishNum; i++)
        {
            Yu yu = Instantiate(fishPre, transform.position + new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range))
                 , Random.rotation).AddComponent<Yu>();
            yu.gameObject.AddComponent<Rigidbody>().useGravity = false;
            yu.tag = thisTag;
            yu.transform.SetParent(transform);
            yu.yuqun = this;
            yuqun.Add(yu);
        }
    }

}
