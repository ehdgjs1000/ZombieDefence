using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardFlip : MonoBehaviour
{
    private bool flipped = false;

    public void ResetFlip()
    {
        transform.DORotate(new(0, flipped ? 180f : 0f, 0), 0.25f);
    }
    public void FlipBtnOnClick()
    {
        if (!flipped)
        {
            transform.DORotate(new(0, flipped ? 0f : 180f, 0), 0.25f);
        }
    }

}
