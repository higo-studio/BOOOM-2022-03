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

    static float GreenRupeeScore = 1;
    static float BlueRupeeScore = 5;
    static float RedRupeeScore = 20;


    void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1).SetId<Tween>("Rotate");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //计入分数
            switch (Type)
            {
                case ItemType.GreenRupee:
                    break;
                case ItemType.BlueRupee:
                    break;
                case ItemType.RedRupee:
                    break;
                default:
                    break;
            }

            DOTween.Kill(gameObject);

            transform.DOLocalJump(transform.position + Vector3.up * 2, 1, 0, 1);
            transform.DORotate(new Vector3(0, 1000, 0), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(() =>
            {
                DOTween.Kill(gameObject);
                Destroy(gameObject);
            });

        }
    }
}
