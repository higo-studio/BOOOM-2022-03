using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WindSys : MonoBehaviour
{
    [Header("≈‰÷√")]

    public ParticleSystem WavePS;

    public GameObject WindVane;

    public GameObject Ship;

    [HideInInspector]
    public float WindForce = 1;

    [HideInInspector]
    public float degree = 0;

    [Space()]

    [Header("ÀÊª˙∑Á¡¶")]

    public bool RandomWindOn = true;

    public float RandomWindCD = 3;

    public Vector2 RandomWindForce;

    //public Vector2 RandomWindDegree;

    public float RandomWindDegreeDelta = 45;

    

    ParticleSystem.VelocityOverLifetimeModule vel;

    public static WindSys instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        vel = WavePS.velocityOverLifetime;
        EffectUpdate();
            StartCoroutine(RandomWind());
        if (RandomWindOn)
        {
            WindForce = Random.Range(RandomWindForce.x, RandomWindForce.y);
            degree = Random.Range(0, 360);
            EffectUpdate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Ship.GetComponent<ShipController>().isAnchor)
        {
            Ship.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Cos(degree * Mathf.PI / 180) * WindForce, 0, Mathf.Sin(degree * Mathf.PI / 180) * WindForce));
        }
    }

    public void EffectUpdate()
    {
        vel.xMultiplier = Mathf.Cos(degree * Mathf.PI / 180) * WindForce * 5;
        vel.zMultiplier = Mathf.Sin(degree * Mathf.PI / 180) * WindForce * 5;

        WindVane.transform.DORotate(new Vector3(0, 0, degree - 90), 1f);
    }

    private IEnumerator RandomWind()
    {
        while (true)
        {
            yield return new WaitForSeconds(RandomWindCD);
            if (RandomWindOn)
            {
                WindForce = Random.Range(RandomWindForce.x, RandomWindForce.y);
                //degree = Random.Range(RandomWindDegree.x, RandomWindDegree.y);
                degree += Random.Range(-RandomWindDegreeDelta, RandomWindDegreeDelta);
                EffectUpdate();
            }
        }
    }
}
