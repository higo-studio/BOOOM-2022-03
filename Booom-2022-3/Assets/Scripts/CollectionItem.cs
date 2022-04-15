using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectionItem : MonoBehaviour
{
    public enum ItemType 
    {
        GreenRupee,
        BlueRupee,
        RedRupee,
    };

    public ItemType Type;

    static int GreenRupeeScore = 5;
    static int BlueRupeeScore = 20;
    static int RedRupeeScore = 100;


    void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1).SetId<Tween>("Rotate");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.GetComponent<AudioSource>().Play();

            //�������
            switch (Type)
            {
                case ItemType.GreenRupee:
                    Score.instance.AddScore(GreenRupeeScore);
                    break;
                case ItemType.BlueRupee:
                    Score.instance.AddScore(BlueRupeeScore);
                    break;
                case ItemType.RedRupee:
                    Score.instance.AddScore(RedRupeeScore);
                    break;
                default:
                    break;
            }

            DOTween.Kill(transform);

            transform.DOLocalJump(transform.position + Vector3.up * 2, 1, 0, 1);
            transform.DORotate(new Vector3(0, 1000, 0), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(() =>
            {
                DOTween.Kill(transform);
                Destroy(gameObject);
            });

        }
    }
}
