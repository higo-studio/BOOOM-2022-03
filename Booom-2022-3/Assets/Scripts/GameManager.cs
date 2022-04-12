using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Tooltip("��������")]
    [Range(0, 10)]
    public float ShipMoveForce;

    [Tooltip("����ת���ٶ�")]
    [Range(0, 10)]
    public float ShipTurnSpeed;

    [Tooltip("��������")]
    [Range(0, 15)]
    public float AccForce;

    [Tooltip("��������ٶ�")]
    [Range(3, 15)]
    public float MaxSpeed;

    [Tooltip("�������ӷ����������")]
    [Range(1, 5)]
    public float RipplePSE;

    private void Awake()
    {
        Instance = this;
    }


}
