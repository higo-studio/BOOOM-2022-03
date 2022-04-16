using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{
    public Rigidbody rb;
    public FishShoalGenerator fishShoalGen;
    public Animator anim;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fishShoalGen = transform.parent.GetComponent<FishShoalGenerator>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.speed = rb.velocity.magnitude / fishShoalGen.maxSpeed;
    }
}
