using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpProfile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nickNameTxt;
    [SerializeField] private TextMeshProUGUI gamerIdTxt;

    public void UpdateNickName()
    {
        nickNameTxt.text = UserInfo.Data.nickName == null ? UserInfo.Data.gamerId : UserInfo.Data.nickName;

        gamerIdTxt.text = UserInfo.Data.gamerId;
    }

}
