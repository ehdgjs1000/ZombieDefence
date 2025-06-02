using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using TMPro;

public class BuyGameSpeed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stateText;

    private IStoreController storeController;

    private string buyGameSpeed = "buygamespeed";


    private void Start()
    {
        InitIAP();
    }
    private void InitIAP()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(buyGameSpeed, ProductType.NonConsumable);
        //UnityPurchasing.Initialize(this, builder);

    }

    public void BuyGameSpeedOnClick(int amount)
    {
        Debug.Log("인앱 성공");
        //todo : amount 만큼 결제 

        if (amount == 2900) BackEndGameData.Instance.UserGameData.promotionType = 3;

        BackEndGameData.Instance.GameDataUpdate();
        LobbyManager.instance.UpdateGameData();
    }
    public void FailedBuyGameSpeed()
    {
        Debug.Log("인앱 실패");

    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError("초기화 실패 : " + error);
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

        if (product.definition.id == buyGameSpeed)
        {
            stateText.text = "게임 2배속 구매 성공";
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
