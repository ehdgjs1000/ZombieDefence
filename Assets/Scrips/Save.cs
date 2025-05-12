using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[System.Serializable]
public class SaveWeapon
{
    public int weaponNum;

    public float fireRate;
    public float maxBulletCount;
    public float damage;
    public float range;
    public float reloadingTime;

    public int weaponLevel;
    public float weaponCount;
    public string weaponName;
    public SaveWeapon(int _weaponNum, float _fireRate, float _maxBulletCount, float _damage, float _range,
        float _realodingTime, int _weaponLevel, float _weaponCount, string _weaponName)
    {
        weaponNum = _weaponNum;

        fireRate = _fireRate;
        maxBulletCount = _maxBulletCount;
        damage = _damage;
        range = _range;
        reloadingTime = _realodingTime;

        weaponLevel = _weaponLevel;
        weaponCount = _weaponCount;

        weaponName = _weaponName;
    }
}
[System.Serializable]
public class SaveEquipedWeapon
{
    public int weaponNum;
    public int slotNum;
    public SaveEquipedWeapon(int _weaponNum, int _slotNum)
    {
        weaponNum= _weaponNum;
        slotNum= _slotNum;
    }
}
public class Save : MonoBehaviour
{
    public static Save instance;

    [SerializeField] public List<SaveWeapon> saveWeaponList;
    [SerializeField] public List<SaveWeapon> loadWeaponList;
    [SerializeField] public WeaponData[] weaponDatas = new WeaponData[14];

    [SerializeField] public List<SaveEquipedWeapon> saveEquipedList;
    [SerializeField] public List<SaveEquipedWeapon> loadEquipedList;
    [SerializeField] public int[] equipedWeaponDatas = new int[3];
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        LoadGameJson();
    }

    public void SaveGameJson()
    {
        SaveWeaponJson();
    }
    public void LoadGameJson()
    {
        LoadWeaponJson();
        LoadEquipedWeaponJson();
    }
    public void SaveEqiopedWeaponJson()
    {
        string equipedWeaponFilePath = Application.persistentDataPath + "/EquipedWeaponData.json";
        string equipedWeaponData = null;
        saveEquipedList.Clear();
        File.WriteAllText(equipedWeaponFilePath, null);

        for (int a = 0; a < LobbyManager.instance.chooseArmyGos.Length; a++)
        {
            if (LobbyManager.instance.chooseArmyGos[a] == null)
            { 

            }
            else
            {
                saveEquipedList.Add(new SaveEquipedWeapon(LobbyManager.instance.chooseArmyGos[a].ReturnArmyWeaponData(), a));
            }
        }
        equipedWeaponData = JsonConvert.SerializeObject(saveEquipedList, Formatting.Indented);
        File.WriteAllText(equipedWeaponFilePath, equipedWeaponData);
    }
    public void LoadEquipedWeaponJson()
    {
        string equipedWeaponFilePath = Application.persistentDataPath + "/EquipedWeaponData.json";
        string equipedWeaponJsonString = File.ReadAllText(equipedWeaponFilePath);

        loadEquipedList = JsonConvert.DeserializeObject<List<SaveEquipedWeapon>>(equipedWeaponJsonString);
        if (loadEquipedList != null)
        {
            for (int weaponNum = 0; weaponNum < loadEquipedList.Count; weaponNum++)
            {
                //Json에서 아이템 정보를 읽어왔을 경우
                if (loadEquipedList[weaponNum] != null)
                {
                    //Lobbymanager에서 장착하기
                    LobbyManager.instance.LoadChooseArmy(loadEquipedList[weaponNum].slotNum,
                        loadEquipedList[weaponNum].weaponNum);
                    LobbyManager.instance.chooseArmyCount++;
                }
                else return;
            }
        }
    }
    public void LoadWeaponJson()
    {
        string weaponFilePath = Application.persistentDataPath + "/WeaponData.json";
        string weaponJsonString = File.ReadAllText(weaponFilePath);

        loadWeaponList = JsonConvert.DeserializeObject<List<SaveWeapon>>(weaponJsonString);
        if(loadWeaponList != null)
        {
            for (int weaponNum = 0; weaponNum < loadWeaponList.Count; weaponNum++)
            {
                //Json에서 아이템 정보를 읽어왔을 경우
                if (loadWeaponList[weaponNum] != null)
                {
                    //weapondata에 데이터 넣기
                    weaponDatas[weaponNum].weaponNum = loadWeaponList[weaponNum].weaponNum;
                    weaponDatas[weaponNum].fireRate = loadWeaponList[weaponNum].fireRate;
                    weaponDatas[weaponNum].maxBulletCount = loadWeaponList[weaponNum].maxBulletCount;
                    weaponDatas[weaponNum].damage = loadWeaponList[weaponNum].damage;
                    weaponDatas[weaponNum].range = loadWeaponList[weaponNum].range;

                    weaponDatas[weaponNum].reloadingTime = loadWeaponList[weaponNum].reloadingTime;
                    weaponDatas[weaponNum].weaponLevel = loadWeaponList[weaponNum].weaponLevel;
                    weaponDatas[weaponNum].weaponCount = loadWeaponList[weaponNum].weaponCount;
                }
            }
        }

    }
    public void SaveWeaponJson()
    {
        Debug.Log("Save");
        string weaponFilePath = Application.persistentDataPath + "/WeaponData.json";
        string weaponData = null;
        saveWeaponList.Clear(); 
        File.WriteAllText(weaponFilePath, null);

        for (int a = 0; a < weaponDatas.Length; a++)
        {
            saveWeaponList.Add(new SaveWeapon(weaponDatas[a].weaponNum, weaponDatas[a].fireRate,
                weaponDatas[a].maxBulletCount, weaponDatas[a].damage, weaponDatas[a].range,
                weaponDatas[a].reloadingTime, weaponDatas[a].weaponLevel, weaponDatas[a].weaponCount,
                weaponDatas[a].name));
        }

        weaponData = JsonConvert.SerializeObject(saveWeaponList, Formatting.Indented);
        File.WriteAllText(weaponFilePath, weaponData);
    }
    public void ResetWeaponJson()
    {
        //총기 정보 초기화
        string weaponFilePath = Application.persistentDataPath + "/WeaponData.json";
        string weaponData = null;
        saveWeaponList.Clear();
        File.WriteAllText(weaponFilePath, null);

        //장착했던 종기 정보 초기화
        string equipedWeaponFilePath = Application.persistentDataPath + "/EquipedWeaponData.json";
        string equipedWeaponData = null;
        saveEquipedList.Clear();
        File.WriteAllText(equipedWeaponFilePath, null);
    }


}
