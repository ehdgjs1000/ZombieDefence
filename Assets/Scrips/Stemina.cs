using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Stemina : MonoBehaviour
{
    //UI
    [SerializeField] private TextMeshProUGUI steminaRechargeTimeTxt;

    private DateTime m_AppQuitTime = new DateTime(1970, 1, 1).ToLocalTime();
    private const int MAX_STEMINA = 30;
    public float steminaRechargeInterval = 300.0f;
    private Coroutine m_RechargeTimerCoroutine = null;
    private float m_RechargeRemainTime = 0;

    private void Awake()
    {
        Init();
    }
    public void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            LoadAppQuitTime();
            SetRechargeScheduler();
        }
        else
        {
            SaveAppQuitTime();
            if (m_RechargeTimerCoroutine != null)
            {
                StopCoroutine(m_RechargeTimerCoroutine);
            }
        }
    }
    private void Start()
    {
        LoadAppQuitTime();
    }
    public void OnApplicationQuit()
    {
        SaveAppQuitTime();
    }
    private void Init()
    {
        m_RechargeRemainTime = 0.0f;
        m_AppQuitTime = new DateTime(1970, 1, 1).ToLocalTime();
        steminaRechargeTimeTxt.text = ("05:00");

    }
    private bool SaveAppQuitTime()
    {
        bool result = false;
        try
        {
            var appQuitTime = DateTime.Now.ToLocalTime().ToBinary().ToString();
            PlayerPrefs.SetString("AppQuitTime", appQuitTime);
            PlayerPrefs.SetFloat("RemainTime", m_RechargeRemainTime);
            PlayerPrefs.Save();
            result = true;

        }
        catch (Exception e)
        {
            Debug.LogError("SaveAppQuitTime Failed (" + e.Message + ")");
        }
        return result;

    }

    private bool LoadAppQuitTime()
    {
        bool result = false;
        try
        {
            if (PlayerPrefs.HasKey("AppQuitTime"))
            {
                var appQuitTime = string.Empty;
                appQuitTime = PlayerPrefs.GetString("AppQuitTime");
                m_AppQuitTime = DateTime.FromBinary(Convert.ToInt64(appQuitTime));
                Debug.Log("AppQuitTime : " + m_AppQuitTime);
            }
            result = true;

        }
        catch (Exception e)
        {
            Debug.LogError("LoadAppQuitTimeFailed (" + e.Message + ")");

        }
        return result;

    }
    private void SetRechargeScheduler(Action onFinish = null)
    {
        if (m_RechargeTimerCoroutine != null)
        {
            StopCoroutine(m_RechargeTimerCoroutine);
        }
        //몇분 나가있었는지
        float timeDifferenceInSec = (int)((DateTime.Now.ToLocalTime() - m_AppQuitTime).TotalSeconds);
        
        //나가있는 동안 추가된 스태미너
        float steminaToAdd = timeDifferenceInSec / steminaRechargeInterval;
        BackEndGameData.Instance.UserGameData.energy += (int)steminaToAdd;

        //다음 스태미너까지 남은 시간
        var remainTime = PlayerPrefs.GetFloat("RemainTime") + timeDifferenceInSec % steminaRechargeInterval;



        if (BackEndGameData.Instance.UserGameData.energy < MAX_STEMINA)
        {
            m_RechargeTimerCoroutine = StartCoroutine(DoRechargeTimer(remainTime, onFinish));
        }
        BackEndGameData.Instance.GameDataUpdate();
    }
    private IEnumerator DoRechargeTimer(float remainTime, Action onFinish = null)
    {
        if (remainTime <= 0.01f)
        {
            m_RechargeRemainTime = steminaRechargeInterval;
        }
        else
        {
            m_RechargeRemainTime = steminaRechargeInterval - remainTime;
        }
        SecToTimer(m_RechargeRemainTime);
        steminaRechargeTimeTxt.text = (Mathf.FloorToInt(min).ToString() + ":" + Mathf.FloorToInt(sec).ToString());
        while (m_RechargeRemainTime > 0)
        {
            SecToTimer(m_RechargeRemainTime);
            steminaRechargeTimeTxt.text = (Mathf.FloorToInt(min).ToString() + ":" + Mathf.FloorToInt(sec).ToString());
            m_RechargeRemainTime--;
            yield return new WaitForSeconds(1f);
        }
        if (BackEndGameData.Instance.UserGameData.energy >= MAX_STEMINA)
        {
            BackEndGameData.Instance.UserGameData.energy = MAX_STEMINA;
            m_RechargeRemainTime = 0;
            SecToTimer(m_RechargeRemainTime);
            steminaRechargeTimeTxt.text = ("05:00");

            m_RechargeTimerCoroutine = null;
        }
        else
        {
            m_RechargeTimerCoroutine = StartCoroutine(DoRechargeTimer(steminaRechargeInterval, onFinish));
        }
    }
    float min = 0;
    float sec = 0;
    private void SecToTimer(float totalSec)
    {
        min = totalSec / 60;
        sec = totalSec % 60;

    }


}
