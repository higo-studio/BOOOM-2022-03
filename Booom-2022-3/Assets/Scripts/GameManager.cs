using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Tooltip("船的马力")]
    [Range(1, 10)]
    public float ShipMoveForce;

    [Tooltip("船的转向速度")]
    [Range(1, 10)]
    public float ShipTurnSpeed;

    [Tooltip("加速力量")]
    [Range(5, 15)]
    public float AccForce;

    [Tooltip("船的最大速度")]
    [Range(3, 15)]
    public float MaxSpeed;

    [Tooltip("涟漪粒子发射调整倍数")]
    [Range(1, 5)]
    public float RipplePSE;

    private void Awake()
    {
        Instance = this;
    }


}
