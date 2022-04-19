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
    /// ��С������糡ʱ�����������ָ��״̬
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
            Debug.Log("��ǰ����:" + WindZoneForce + ";����" + WindZoneDegree + "��");
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
            WindSys.instance.RandomWindCD = RandomWindCD;
            WindSys.instance.RandomWindOn = OriRandomOn;

            WindSys.instance.EffectUpdate();
            Debug.Log("��ǰ����:" + OriForce + ";����" + OriDegree + "��");
        }
    }
}
