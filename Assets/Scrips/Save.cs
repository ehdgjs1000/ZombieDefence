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
public class Save : MonoBehaviour
{
    public static Save instance;

    [SerializeField] public List<SaveWeapon> saveWeaponList;
    [SerializeField] public List<SaveWeapon> loadWeaponList;
    [SerializeField] public WeaponData[] weaponDatas = new WeaponData[14];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        LoadGameJson();
    }
    public void SaveGameJson()
    {
        SaveWeaponJson();
    }
    public void LoadGameJson()
    {
        LoadWeaponJson();
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
        Debug.Log(weaponFilePath);
        saveWeaponList.Clear(); 
        File.WriteAllText(weaponFilePath, null);

        Debug.Log("weaponDatasLenght" + " : "+ weaponDatas.Length);
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
        string weaponFilePath = Application.persistentDataPath + "/WeaponData.json";
        string weaponData = null;
        saveWeaponList.Clear();
        File.WriteAllText(weaponFilePath, null);
    }


}
