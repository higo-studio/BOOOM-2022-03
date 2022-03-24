using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    Vector3 move;
    float forwardAmount;
    float turnAmount;
    public float speed = 10;
    public float turnSpeed = 10;
    Rigidbody rb;
    public float pseRate = 1;
    public ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

 
        move = new Vector3(x, 0, z);
        Vector3 localMove = transform.InverseTransformVector(move);
        if (localMove.magnitude > 1f) localMove = localMove.normalized;

        forwardAmount = localMove.z;
        turnAmount = Mathf.Atan2(localMove.x, localMove.z);
        //transform.GetChild(0).GetChild(0).localRotation = Quaternion.Euler(0, 0, -turnAmount * 10);

        //�޸��˻����ӷ�������
        var emission = particle.emission;
        emission.rateOverTime = 1 + rb.velocity.magnitude * pseRate;

    }

    private void FixedUpdate()
    {
        rb.AddForce(forwardAmount * transform.forward * speed);
        rb.MoveRotation(GetComponent<Rigidbody>().rotation * Quaternion.Euler(0, turnAmount * turnSpeed, 0));

    }
}
