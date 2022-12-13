using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemBase : MonoBehaviour
{
    public int Index;

    private void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DORotateQuaternion(Quaternion.Euler(new Vector3(-90, 180f, 0)), 2).SetEase(Ease.Linear))
                .SetLoops(-1, LoopType.Restart);
    }

    protected virtual void OnTriggerEnter(Collider collision)
    {

    }
}
