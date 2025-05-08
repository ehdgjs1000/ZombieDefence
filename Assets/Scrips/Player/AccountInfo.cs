using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountInfo : MonoBehaviour
{
    public static AccountInfo instance;
    [SerializeField] private UserInfo user;

    public int Gold;
    public int Crystal;

    [Header("Gun Info")]
    public float[] pistolCount;
    public float[] smgCount;
    public float[] rifleCount;
    public float[] srCount;
    public float[] dmrCount;
    public float[] specialCount;
    public int[] pistolLevel;
    public int[] rifleLevel;
    public int[] smgLevel;
    public int[] srLevel;
    public int[] dmrLevel;
    public int[] specialLevel;

    public bool isLogIn = true;
    public float[] questCount = new float[6];
    public bool[] isClear = new bool[7] {false, false, false, false, false, false, false};


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        BackEndGameData.Instance.GameDataLoad();
        user.GetUserInfoFromBackEnd();
    }
    private void Start()
    {
        StartCoroutine(AccountInfoAwake());
    }
    public IEnumerator AccountInfoAwake()
    {
        Debug.Log("Called");
        yield return new WaitForSeconds(0.1f);
        BackEndGameData.Instance.GameDataUpdate();
        Gold = BackEndGameData.Instance.UserGameData.gold;
        Crystal = BackEndGameData.Instance.UserGameData.crystal;
    }
    public void SyncAccountToBackEnd()
    {
        BackEndGameData.Instance.UserGameData.gold = Gold;
        BackEndGameData.Instance.UserGameData.crystal = Crystal;
        BackEndGameData.Instance.GameDataUpdate();
    }
    public int CashInfo(int type) //#0 Gold #1 Crystal
    {
        if (type == 0) return Gold;
        else if (type == 1) return Crystal;
        else return 999;
    }

}
