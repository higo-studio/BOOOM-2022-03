using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FishShoalGenerator : MonoBehaviour
{
    public int fishCount;
    public GameObject fishPre;
    public Vector3 positiveRange;//生成范围的正方向
    public Vector3 negativeRange;//生成范围的负方向
    public string thisTag;
    public string foodTag;//找食物
    public string enemyTag;//敌人
    public float escapeDis;//逃跑检测距离
    public float escapeWeight;//逃跑力度
    public float predationDis;//捕食距离
    public float predationWeight;//捕食力度
    public float keepCreaterWeight;//鱼群中心？
    public float findFriendDis;
    public float toFriendsCenterWeight;
    public float keepFriendDis;//跟随距离
    public float keepFriendDisWeight;//跟随力度
    public float keepFriendSpeedWeight;//跟随速度，越大鱼群越统一
    public float randWeight;//随机，越大越混乱
    public float maxSpeed;
    public float maxTurnSpeed;
    public float maxAcceleration;

    public float minCD;
    public float maxCD;

    public List<fishShoalManager> fishShoal;

    void Start()
    {
        fishShoal = new List<fishShoalManager>();
        for (int i = 0; i < fishCount; i++)
        {
            fishShoalManager newFishShoal = Instantiate(fishPre, transform.position
                + new Vector3(Random.Range(-negativeRange.x, positiveRange.x), Random.Range(-negativeRange.y, positiveRange.y), Random.Range(-negativeRange.z, positiveRange.z))
                , Random.rotation).AddComponent<fishShoalManager>();

            newFishShoal.gameObject.AddComponent<Rigidbody>().useGravity = false;
            newFishShoal.tag = thisTag;
            newFishShoal.transform.SetParent(transform);
            newFishShoal.fishShoalGen = this;

            fishShoal.Add(newFishShoal);
        }
    }

}
