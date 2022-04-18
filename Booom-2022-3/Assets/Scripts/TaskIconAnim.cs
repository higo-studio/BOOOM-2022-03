using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TaskIconAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

}
