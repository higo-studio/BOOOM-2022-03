using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    public float WindZoneDegree = 90;
    public float WindZoneForce = 1;
    public float RandomWindCD = 60;
    public bool isRandomWindOn;

    float OriDegree;
    float OriForce;
    float OriCD;
    bool OriRandomOn;

    /// <summary>
    /// 当小船进入风场时，将风力变成指定状态
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OriDegree = WindSys.instance.degree;
            OriForce = WindSys.instance.WindForce;
            OriCD = WindSys.instance.RandomWindCD;
            OriRandomOn = WindSys.instance.RandomWindOn;

            WindSys.instance.degree = WindZoneDegree;
            WindSys.instance.WindForce = WindZoneForce;
            WindSys.instance.RandomWindCD = RandomWindCD;
            WindSys.instance.RandomWindOn = isRandomWindOn;

            WindSys.instance.EffectUpdate();
            Debug.Log("当前风力:" + WindZoneForce + ";风向" + WindZoneDegree + "°");
        }
    }

    /// <summary>
    /// 当小船离开风场时，将风力变成普通状态
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            WindSys.instance.degree = OriDegree;
            WindSys.instance.WindForce = OriForce;
            WindSys.instance.RandomWindCD = RandomWindCD;
            WindSys.instance.RandomWindOn = OriRandomOn;

            WindSys.instance.EffectUpdate();
            Debug.Log("当前风力:" + OriForce + ";风向" + OriDegree + "°");
        }
    }
}
