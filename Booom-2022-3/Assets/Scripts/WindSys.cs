using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSys : MonoBehaviour
{
    public ParticleSystem WavePS;

    public GameObject WindVane;

    public GameObject Ship;

    public float WindForce = 1;

    public float degree = 0;

    ParticleSystem.VelocityOverLifetimeModule vel;
    // Start is called before the first frame update
    void Start()
    {
        vel = WavePS.velocityOverLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Ship.GetComponent<ShipController>().isAnchor)
        {
            Ship.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Cos(degree * Mathf.PI / 180) * WindForce, 0, Mathf.Sin(degree * Mathf.PI / 180) * WindForce));
        }

        vel.xMultiplier = Mathf.Cos(degree * Mathf.PI / 180) * WindForce * 5;
        vel.zMultiplier = Mathf.Sin(degree * Mathf.PI / 180) * WindForce * 5;

        WindVane.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, degree-90);
    }
}
