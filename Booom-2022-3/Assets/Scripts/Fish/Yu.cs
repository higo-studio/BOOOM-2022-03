using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yu : MonoBehaviour
{
    [HideInInspector]
    public YuQun yuqun;
    private Rigidbody rb;
    public float cd;
    public float t;
    private Vector3 randVelocity;
    private Vector3 curVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Compute();
        rb.velocity = Vector3.RotateTowards(rb.velocity, curVelocity, yuqun.maxTurnSpeed * Time.deltaTime, yuqun.maxAcceleration * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    public void Compute()
    {
        RandomVelocity();
        List<Yu> friends = GetFriends();
        Vector3 v3 = PredationVelocity() + EscapeVelocity() + KeepFriendsVelocity(friends) + KeepCreaterDisVelocity()
            + ToCenterVelocity(friends) + KeepFriendsDisVelocity(friends) + randVelocity;
        v3 /= 7 * yuqun.maxSpeed;
        v3 = ClampVelocity(v3);
        curVelocity = v3;
    }

    void RandomVelocity()
    {
        t -= 0.02f;
        if (t <= 0)
        {
            t = cd;
            cd = Random.Range(yuqun.minCD, yuqun.maxCD);
            randVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * yuqun.randWeight;
        }
    }

    List<Yu> GetFriends()
    {
        GameObject[] friends = GameObject.FindGameObjectsWithTag(yuqun.thisTag);
        List<Yu> friendsList = new List<Yu>();
        for (int i = 0; i < friends.Length; i++)
        {
            if (friends[i] == gameObject) continue;
            if (Vector3.Distance(friends[i].transform.position, transform.position) < yuqun.findFriendDis)
            {
                friendsList.Add(friends[i].GetComponent<Yu>());
            }
        }
        return friendsList;
    }

    Vector3 KeepCreaterDisVelocity()
    {
        return (yuqun.transform.position - transform.position) * 0.1f * yuqun.keepCreaterWeight;
    }

    Vector3 KeepFriendsVelocity(List<Yu> friends)
    {
        Vector3 friendsVel = Vector3.zero;
        if (friends.Count == 0) return friendsVel;
        foreach (var item in friends)
        {
            friendsVel += item.rb.velocity;
        }
        friendsVel /= friends.Count;
        return friendsVel.normalized * yuqun.keepFriendSpeedWeight;
    }

    Vector3 ToCenterVelocity(List<Yu> friends)
    {
        Vector3 center = Vector3.zero;
        if (friends.Count == 0) return center;
        foreach (var item in friends)
        {
            center += item.transform.position;
        }
        center /= friends.Count;
        return (center - transform.position).normalized * yuqun.toFriendsCenterWeight;
    }

    Vector3 KeepFriendsDisVelocity(List<Yu> friends)
    {
        float dis = 999;
        Yu yu = null;
        foreach (var item in friends)
        {
            float d = Vector3.Distance(transform.position, item.transform.position);
            if (d < dis)
            {
                dis = d;
                yu = item;
            }
        }
        if (yu != null)
        {
            return (yu.transform.position - transform.position).normalized * Mathf.Clamp((dis - yuqun.keepFriendDis) / yuqun.keepFriendDis, -1f, 1f) * yuqun.keepFriendDisWeight;
        }
        else
        {
            return Vector3.zero;
        }
    }

    Vector3 EscapeVelocity()
    {
        if (string.IsNullOrEmpty(yuqun.enemyTag)) return Vector3.zero;
        GameObject[] gos = GameObject.FindGameObjectsWithTag(yuqun.enemyTag);
        float dis = 9999;
        GameObject yu = null;
        foreach (var item in gos)
        {
            float d = Vector3.Distance(transform.position, item.transform.position);
            if (d < dis)
            {
                dis = d;
                yu = item;
            }
        }
        if (yu != null)
        {
            float xd = Mathf.Clamp((yuqun.escapeDis - dis) / yuqun.escapeDis, 0f, 1f) * 1.5f;
            return (transform.position - yu.transform.position).normalized
                * xd * xd * yuqun.escapeWeight;
        }
        else
        {
            return Vector3.zero;
        }
    }

    Vector3 PredationVelocity()
    {
        if (string.IsNullOrEmpty(yuqun.foodTag)) return Vector3.zero;
        GameObject[] gos = GameObject.FindGameObjectsWithTag(yuqun.foodTag);
        float dis = 9999;
        GameObject yu = null;
        foreach (var item in gos)
        {
            float d = Vector3.Distance(transform.position, item.transform.position);
            if (d < dis)
            {
                dis = d;
                yu = item;
            }
        }
        if (yu != null)
        {
            float xd = Mathf.Clamp((yuqun.predationDis - dis) / yuqun.predationDis, 0f, 1f) * 1.5f;
            return (yu.transform.position - transform.position).normalized
                * xd * xd * yuqun.predationWeight;
        }
        else
        {
            return Vector3.zero;
        }
    }

    Vector3 ClampVelocity(Vector3 velocity)
    {
        return Vector3.ClampMagnitude(velocity, yuqun.maxSpeed);
    }
}
