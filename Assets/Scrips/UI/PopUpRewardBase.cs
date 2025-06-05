using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PopUpRewardBase : MonoBehaviour
{
    public static PopUpRewardBase instance;
    private bool isPoping = false;

    [SerializeField] GameObject goldText;
    [SerializeField] GameObject crystalText;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    protected void ResetUI()
    {
        goldText.GetComponent<TextMeshProUGUI>().text = string.Empty;
        crystalText.GetComponent<TextMeshProUGUI>().text = string.Empty;
    }
    public void SetMessage(int _goldAmount, int _crystalAmount)
    {
        goldText.GetComponent<TextMeshProUGUI>().text = _goldAmount.ToString();
        crystalText.GetComponent<TextMeshProUGUI>().text = _crystalAmount.ToString();
        if (!isPoping) StartCoroutine(ShowMessage());
    }
    IEnumerator ShowMessage()
    {
        //메세지 팝업
        isPoping = true;
        this.transform.DOScale(Vector3.one * 1, 0.3f);
        yield return new WaitForSeconds(1.5f);
        //메세지 없애기
        this.transform.DOScale(Vector3.zero, 0.3f);
        yield return new WaitForSeconds(0.1f);
        isPoping = false;
        ResetUI();
    }
}
