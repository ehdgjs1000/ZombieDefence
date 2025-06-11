using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image loadingProgressImg;
    [SerializeField] private TextMeshProUGUI    progressTxt;
    [SerializeField] private float progressTime;

    public void Play(UnityAction action = null)
    {
        StartCoroutine(OnProgress(action));
    }
    IEnumerator OnProgress(UnityAction action)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1 )
        { 
            current += Time.deltaTime;
            percent = current / progressTime;

            //progressTxt.text = $"Loading... {loadingProgressImg.fillAmount * 100:F0}%";
            loadingProgressImg.fillAmount = Mathf.Lerp(0,1,percent);
            
            yield return null;

        }
        action?.Invoke();
    }


}
