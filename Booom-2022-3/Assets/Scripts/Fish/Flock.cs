using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public float speed = 0.001f;
    public float rotationSpeed = 4.0f;//ת���ٶ�

    public float minSpeed = 0.5f;
    public float maxSpeed = 1f;

    Vector3 averageHeading;
    Vector3 averagePosition;
    public float neighbourDistance = 3.0f;//�ж��Ƿ���ľ���
    public float avoidDistance = 1.0f;//��Ⱥ�и���ļ������

    public Vector3 newGoalPos;
    bool turning = false;//����ת��

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
        gos = GlobalFlock.allFish;//Ⱥ��

        Vector3 vcentre = Vector3.zero;//����
        Vector3 vavoid = Vector3.zero;//�ر�����
        float gSpeed = 0.1f;//Ⱥ���ٶ�

        Vector3 goalPos = GlobalFlock.goalPos;
        float dist;//����
        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);//������Ⱥ�ľ���
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
