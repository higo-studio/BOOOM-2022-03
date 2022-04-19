using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine; 
using System;
//using XInputDotNetPure; // Required in C#

public class ShipController : MonoBehaviour
{
    Vector3 move;
    float forwardAmount;
    float turnAmount;
    public float speed;
    public float AccSpeed;
    float OriginSpeed;
    bool isAcc = false;
    public float turnSpeed;
    Rigidbody rb;
    public float pseRate;
    public ParticleSystem particle;
    public Image AccCD;
    public float MaxSpeed;
    public Transform mapPlayerSign;

    //bool playerIndexSet = false;

    public bool isAnchor;//抛锚
    /*
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    */
    //float shakeForce = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        OriginSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
        //GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);
        */

        Anchor();
        if (!isAnchor)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            move = new Vector3(x, 0, z);

            Accelerate();
        }

        Vector3 localMove = transform.InverseTransformVector(move);
        if (localMove.magnitude > 1f) localMove = localMove.normalized;

        forwardAmount = localMove.z;
        turnAmount = Mathf.Atan2(localMove.x, localMove.z);
        //transform.GetChild(0).GetChild(0).localRotation = Quaternion.Euler(0, 0, -turnAmount * 10);

        var emission = particle.emission;
        emission.rateOverTime = 1 + rb.velocity.magnitude * pseRate;

        //改变地图标志的方向
        mapPlayerSign.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -transform.eulerAngles.y);


    }

    private void Anchor()
    {
        if (Input.GetButtonDown("Anchor"))
        {
            isAnchor = !isAnchor;
        }
        if (isAnchor)
        {
            move = Vector3.zero;
        }

    }

    private void FixedUpdate()
    {
        if (!isAcc)
        {
            if (rb.velocity.magnitude <= MaxSpeed)
                rb.AddForce(forwardAmount * transform.forward * speed);
        }
        else
        {
            rb.AddForce(forwardAmount * transform.forward * speed);
        }

        rb.MoveRotation(GetComponent<Rigidbody>().rotation * Quaternion.Euler(0, turnAmount * turnSpeed, 0));

    }


    void Accelerate()
    {
        //float timeCount = 1f;
        if (Input.GetButtonDown("Accelerate") && !isAcc)
        {
            speed = AccSpeed;
            rb.velocity = transform.forward * speed;
            isAcc = true;
            AccCD.fillAmount = 1;
            /*
            GamePad.SetVibration(playerIndex, shakeForce, shakeForce);
            DOTween.To(() => timeCount, a => timeCount = a, 0.1f, 0.2f).OnComplete(() =>
            {
                timeCount = 1f;
                GamePad.SetVibration(playerIndex, 0, 0);
            });
            */
            DOTween.To(() => speed, x => speed = x, OriginSpeed, 3f);
            DOTween.To(() => AccCD.fillAmount, y => AccCD.fillAmount = y, 0, 3.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                isAcc = false;
            });

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //手柄震动
        /*
        float timeCount = 1f;
        GamePad.SetVibration(playerIndex, shakeForce, shakeForce);
        DOTween.To(() => timeCount, a => timeCount = a, 0.1f, 0.2f).OnComplete(() =>
        {
            timeCount = 1f;
            GamePad.SetVibration(playerIndex, 0, 0);
        });
        */
        //镜头抖动
        var impulseSource = transform.GetComponent<CinemachineImpulseSource>();
        impulseSource.GenerateImpulseWithVelocity(Vector3.one * 0.05f);
        //播放音效
        var audioSource = transform.Find("SFX").GetComponent<AudioSource>();
        audioSource.pitch = UnityEngine.Random.Range(0.5f, 1f);
        audioSource.Play();


    }
}
