using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginBase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMessage;

    protected void ResetUI(params Image[] images)
    {
        textMessage.text = string.Empty;
        for (int a = 0; a < images.Length; a++)
        {
            images[a].color = Color.white;
        }
    }

    protected void SetMessage(string message)
    {
        textMessage.text = message;
    }

    protected void GuideForIncorrectEnterData(Image image, string message)
    {
        textMessage.text = message;
        image.color = Color.red;
    }
    protected bool IsFieldDataEmpty(Image image, string field, string result)
    {
        if (field.Trim().Equals(""))
        {
            GuideForIncorrectEnterData(image, $"\"{result}\" 필드를 채워주세요.");
            return true;
        }

        return false;
    }

}
