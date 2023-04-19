using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PacManMonthAnimation : MonoBehaviour
{
    [SerializeField] float SpentTimeForMonthMove = 0.2f;
    [SerializeField] bool isRightMonth = true;
    float PlusOrMinus = 1;

    private void Start()
    {
        if(!isRightMonth)
        {
            PlusOrMinus = -1;
        }

        transform.DOLocalRotate(PlusOrMinus * Vector3.up * 160, SpentTimeForMonthMove)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }
}
