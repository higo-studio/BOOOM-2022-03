using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    public float WindZoneDegree = 90;
    public float WindZoneForce = 1;

    float OriDegree;
    float OriForce;
    bool OriRandomOn;

    /// <summary>
    /// ��С������糡ʱ�����������ָ��״̬
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            OriDegree = WindSys.instance.degree;
            OriForce = WindSys.instance.WindForce;
            OriRandomOn = WindSys.instance.RandomWindOn;

            WindSys.instance.degree = WindZoneDegree;
            WindSys.instance.WindForce = WindZoneForce;
            WindSys.instance.RandomWindOn = false;

            WindSys.instance.EffectUpdate();
        }
    }

    /// <summary>
    /// ��С���뿪�糡ʱ�������������ͨ״̬
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            WindSys.instance.degree = OriDegree;
            WindSys.instance.WindForce = OriForce;
            WindSys.instance.RandomWindOn = OriRandomOn;

            WindSys.instance.EffectUpdate();
        }
    }
}
