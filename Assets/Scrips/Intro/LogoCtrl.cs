using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LogoCtrl : MonoBehaviour
{
    private void Start()
    {
        LogoScale();
        //StartCoroutine(LogoRotate());
    }
    private void LogoScale()
    {
        transform.DOScale(new Vector3(1,1,1), 3f);
    }
    /*private IEnumerator LogoRotate()
    {
        transform.DORotate(new Vector3(0,0,180), 1.0f);
        yield return new WaitForSeconds(0.2f);
        transform.DORotate(new Vector3(0, 0, 360), 1.0f);
    }*/
}
