using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using TMPro;

public class BuyCrystal : MonoBehaviour, IStoreListener
{
    [SerializeField] private TextMeshProUGUI stateText;

    private IStoreController storeController;

    private string crystal3000 = "crystal3000";
    private string crystal9900 = "crystal9900";
    private string crystal49000 = "crystal49000";

    private void Start()
    {
        InitIAP();
    }
    private void InitIAP()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(crystal3000, ProductType.Consumable);
        builder.AddProduct(crystal9900, ProductType.Consumable);
        builder.AddProduct(crystal49000, ProductType.Consumable);

        //UnityPurchasing.Initialize(this, builder);

    }

    public void BuyCrystalOnClick(int amount)
    {
        Debug.Log("인앱 성공");
        //todo : amount 만큼 결제 

        if (amount == 3000) BackEndGameData.Instance.UserGameData.crystal += 900;
        else if (amount == 9900) BackEndGameData.Instance.UserGameData.crystal += 3500;
        else if (amount == 49000) BackEndGameData.Instance.UserGameData.crystal += 13000;

        BackEndGameData.Instance.GameDataUpdate();
    }
    public void FailedBuyCrystal()
    {
        Debug.Log("인앱 실패");

    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError("초기화 실패 : "  + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError("초기화 실패 : " + error + " : " + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("구매 실패");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        Debug.Log("구매 성공 : " + product.definition.id);

        if (product.definition.id == crystal3000)
        {
            stateText.text = "크리스탈 900개 구입 성공";
        }else if (product.definition.id == crystal9900)
        {
            stateText.text = "크리스탈 3500개 구입 성공";
        }else if (product.definition.id == crystal49000)
        {
            stateText.text = "크리스탈 13000개 구입 성공";
        }

        return PurchaseProcessingResult.Complete;
    }
    public void Purchase(string productID)
    {
        storeController.InitiatePurchase(productID);
    }
    private void CheckNonConsumable(string id)
    {
        //구매 영수증 확인
        var product = storeController.products.WithID(id);

        if (product != null)
        {
            bool isCheck = product.hasReceipt;
        }

    }

}
