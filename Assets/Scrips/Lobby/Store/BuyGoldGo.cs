using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyGoldGo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldAmountText;
    [SerializeField] TextMeshProUGUI crystalAmountText;
    [SerializeField] TextMeshProUGUI buyCrystalAmountText;
    [SerializeField] TextMeshProUGUI kwrAmountText;

    private float goldAmount;
    private float crystalAmount;
    private float krwAmount;
    public void UpdatePurchaseGoldInfo(float _crystalA, float _goldA)
    {
        goldAmount = _goldA;
        crystalAmount = _crystalA;

        goldAmountText.text = goldAmount.ToString();
        crystalAmountText.text = crystalAmount.ToString();
    }
    public void UpdatePurchaseCrystalInfo(float _KWR, float _crystal)
    {
        crystalAmount = _crystal;
        krwAmount = _KWR;

        buyCrystalAmountText.text = _crystal.ToString();
        kwrAmountText.text = krwAmount.ToString();

    }

}
