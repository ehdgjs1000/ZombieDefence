using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public static LevelUp instance;
    private RectTransform levelUpUi;


    private void Awake()
    {
        instance = this;
        levelUpUi = GetComponent<RectTransform>();
    }
    public void ShowLevelUp()
    {
        levelUpUi.localScale = Vector3.one;
        GameManager.instance.StopGame();
    }
    public void HideLevelUp()
    {
        levelUpUi.localScale = Vector3.zero;
        this.gameObject.SetActive(false);
        GameManager.instance.ResumeGame();
    }
}