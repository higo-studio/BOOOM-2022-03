using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public float speed = 0.001f;
    public float rotationSpeed = 4.0f;//转向速度

    public float minSpeed = 0.5f;
    public float maxSpeed = 1f;

    Vector3 averageHeading;
    Vector3 averagePosition;
    public float neighbourDistance = 3.0f;//判断是否伙伴的距离
    public float avoidDistance = 1.0f;//鱼群中个体的间隔距离

    public Vector3 newGoalPos;
    bool turning = false;//碰到转向

    GlobalFlock globalFlock;
    Animator anim;
    void Start()
    {
        globalFlock = GetComponentInParent<GlobalFlock>();
        anim = GetComponentInParent<Animator>();
        speed = Random.Range(minSpeed, maxSpeed);
    }


    void OnTriggerEnter(Collider other)
    {
        if (!turning)
        {
            newGoalPos = this.transform.position - other.transform.position;
        }
        turning = true;
    }

    void OnTriggerExit(Collider other)
    {
        turning = false;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, globalFlock.transform.position) >= Vector3.Distance(globalFlock.tankSize, Vector3.zero))
        {
            turning = true;
        }
        else
        {
            turning = false;
        }

        if (turning)
        {
            Vector3 direction = newGoalPos - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction),
                rotationSpeed * Time.deltaTime);

            speed = Random.Range(minSpeed, maxSpeed);
        }
        else
        {
            if (Random.Range(0, 10) < 1)
            {
                ApplyRules();
            }
        }

        transform.Translate(0, 0, Time.deltaTime * speed);
        anim.speed = Mathf.Sqrt(speed) / 2;

    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = GlobalFlock.allFish;//群组

        Vector3 vcentre = Vector3.zero;//中心
        Vector3 vavoid = Vector3.zero;//回避向量
        float gSpeed = 0.1f;//群组速度

        Vector3 goalPos = GlobalFlock.goalPos;
        float dist;//距离
        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);//距离鱼群的距离
                if (dist <= neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if (dist < 1f)
                    {
                        vavoid += (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed += anotherFlock.speed;

                }
            }
        }

        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            speed = Mathf.Lerp(speed, gSpeed / groupSize, Time.deltaTime * gSpeed);
            anim.speed = Mathf.Sqrt(speed) / 2;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(direction),
                    rotationSpeed * Time.deltaTime);
            }
        }
    }
}
