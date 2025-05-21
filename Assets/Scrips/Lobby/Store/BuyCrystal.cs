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
        Debug.Log("�ξ� ����");
        //todo : amount ��ŭ ���� 

        if (amount == 3000) BackEndGameData.Instance.UserGameData.crystal += 900;
        else if (amount == 9900) BackEndGameData.Instance.UserGameData.crystal += 3500;
        else if (amount == 49000) BackEndGameData.Instance.UserGameData.crystal += 13000;

        BackEndGameData.Instance.GameDataUpdate();
    }
    public void FailedBuyCrystal()
    {
        Debug.Log("�ξ� ����");

    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError("�ʱ�ȭ ���� : "  + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError("�ʱ�ȭ ���� : " + error + " : " + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("���� ����");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        Debug.Log("���� ���� : " + product.definition.id);

        if (product.definition.id == crystal3000)
        {
            stateText.text = "ũ����Ż 900�� ���� ����";
        }else if (product.definition.id == crystal9900)
        {
            stateText.text = "ũ����Ż 3500�� ���� ����";
        }else if (product.definition.id == crystal49000)
        {
            stateText.text = "ũ����Ż 13000�� ���� ����";
        }

        return PurchaseProcessingResult.Complete;
    }
    public void Purchase(string productID)
    {
        storeController.InitiatePurchase(productID);
    }
    private void CheckNonConsumable(string id)
    {
        //���� ������ Ȯ��
        var product = storeController.products.WithID(id);

        if (product != null)
        {
            bool isCheck = product.hasReceipt;
        }

    }

}
