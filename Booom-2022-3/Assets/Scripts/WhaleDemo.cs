using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WhaleDemo : MonoBehaviour
{
    public float duration = 180;
    public Transform[] pathPoint;
    Vector3[] pathPos;
    int PathIndex = 0;

    void Start()
    {
        //transform.DOLocalMove(transform.localPosition + Vector3.forward * -200, 40f);
        pathPos = new Vector3[pathPoint.Length+1];
        for (int i = 0; i < pathPoint.Length; i++)
        {
            pathPos[i] = pathPoint[i].position;
        }

        pathPos[pathPoint.Length] = transform.position;
        transform.DOPath(pathPos, duration, PathType.CatmullRom, PathMode.TopDown2D, 10, Color.yellow).OnWaypointChange(p => { MoveOver(pathPos); }).SetEase(Ease.Linear).SetLoops(-1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pathPos"></param>
    public void MoveOver(Vector3[] pathPos)
    {
        transform.DOLookAt(pathPos[PathIndex], 1f);
        PathIndex += 1;
        if (PathIndex >= pathPoint.Length + 1) PathIndex = 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.GetComponent<AudioSource>().Play();
        }
    }
}
