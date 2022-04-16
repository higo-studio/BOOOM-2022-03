using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishShoalManager : MonoBehaviour
{
    [HideInInspector]
    public FishShoalGenerator fishShoalGen;
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
        rb.velocity = Vector3.RotateTowards(rb.velocity, curVelocity, fishShoalGen.maxTurnSpeed * Time.deltaTime, fishShoalGen.maxAcceleration * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    public void Compute()
    {
        RandomVelocity();
        List<fishShoalManager> friends = GetFriends();
        Vector3 v3 = PredationVelocity() + EscapeVelocity() + KeepFriendsVelocity(friends) + KeepCreaterDisVelocity()
            + ToCenterVelocity(friends) + KeepFriendsDisVelocity(friends) + randVelocity;
        v3 /= 7 * fishShoalGen.maxSpeed;
        v3 = ClampVelocity(v3);
        curVelocity = v3;
    }

    void RandomVelocity()
    {
        t -= 0.02f;
        if (t <= 0)
        {
            t = cd;
            cd = Random.Range(fishShoalGen.minCD, fishShoalGen.maxCD);
            randVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * fishShoalGen.randWeight;
        }
    }

    List<fishShoalManager> GetFriends()
    {
        GameObject[] friends = GameObject.FindGameObjectsWithTag(fishShoalGen.thisTag);
        List<fishShoalManager> friendsList = new List<fishShoalManager>();
        for (int i = 0; i < friends.Length; i++)
        {
            if (friends[i] == gameObject) continue;
            if (Vector3.Distance(friends[i].transform.position, transform.position) < fishShoalGen.findFriendDis)
            {
                friendsList.Add(friends[i].GetComponent<fishShoalManager>());
            }
        }
        return friendsList;
    }

    Vector3 KeepCreaterDisVelocity()
    {
        return (fishShoalGen.transform.position - transform.position) * 0.1f * fishShoalGen.keepCreaterWeight;
    }

    Vector3 KeepFriendsVelocity(List<fishShoalManager> friends)
    {
        Vector3 friendsVel = Vector3.zero;
        if (friends.Count == 0) return friendsVel;
        foreach (var item in friends)
        {
            friendsVel += item.rb.velocity;
        }
        friendsVel /= friends.Count;
        return friendsVel.normalized * fishShoalGen.keepFriendSpeedWeight;
    }

    Vector3 ToCenterVelocity(List<fishShoalManager> friends)
    {
        Vector3 center = Vector3.zero;
        if (friends.Count == 0) return center;
        foreach (var item in friends)
        {
            center += item.transform.position;
        }
        center /= friends.Count;
        return (center - transform.position).normalized * fishShoalGen.toFriendsCenterWeight;
    }

    Vector3 KeepFriendsDisVelocity(List<fishShoalManager> friends)
    {
        float dis = 999;
        fishShoalManager yu = null;
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
            return (yu.transform.position - transform.position).normalized * Mathf.Clamp((dis - fishShoalGen.keepFriendDis) / fishShoalGen.keepFriendDis, -1f, 1f) * fishShoalGen.keepFriendDisWeight;
        }
        else
        {
            return Vector3.zero;
        }
    }

    Vector3 EscapeVelocity()
    {
        if (string.IsNullOrEmpty(fishShoalGen.enemyTag)) return Vector3.zero;
        GameObject[] gos = GameObject.FindGameObjectsWithTag(fishShoalGen.enemyTag);
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
            float xd = Mathf.Clamp((fishShoalGen.escapeDis - dis) / fishShoalGen.escapeDis, 0f, 1f) * 1.5f;
            return (transform.position - yu.transform.position).normalized
                * xd * xd * fishShoalGen.escapeWeight;
        }
        else
        {
            return Vector3.zero;
        }
    }

    Vector3 PredationVelocity()
    {
        if (string.IsNullOrEmpty(fishShoalGen.foodTag)) return Vector3.zero;
        GameObject[] gos = GameObject.FindGameObjectsWithTag(fishShoalGen.foodTag);
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
            float xd = Mathf.Clamp((fishShoalGen.predationDis - dis) / fishShoalGen.predationDis, 0f, 1f) * 1.5f;
            return (yu.transform.position - transform.position).normalized
                * xd * xd * fishShoalGen.predationWeight;
        }
        else
        {
            return Vector3.zero;
        }
    }

    Vector3 ClampVelocity(Vector3 velocity)
    {
        return Vector3.ClampMagnitude(velocity, fishShoalGen.maxSpeed);
    }
}
