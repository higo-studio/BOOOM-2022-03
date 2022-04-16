using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmCtrl : MonoBehaviour
{
    public Rigidbody rb;
    public YuQun yuqun;
    public Animator am;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        yuqun = transform.parent.GetComponent<YuQun>();
        am = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        am.speed = rb.velocity.magnitude / yuqun.maxSpeed * 5;
    }
}
