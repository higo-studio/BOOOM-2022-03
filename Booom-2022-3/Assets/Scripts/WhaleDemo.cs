using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WhaleDemo : MonoBehaviour
{
    void Start()
    {
        transform.DOLocalMove(transform.localPosition + Vector3.forward * -200, 40f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.GetComponent<AudioSource>().Play();
        }
    }
}
