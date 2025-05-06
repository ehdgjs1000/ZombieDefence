using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Weapon", menuName ="Scriptable Object/WeaponData")]
public class WeaponData : ScriptableObject
{
    public enum WeaponType
    {
        Pistol,
        SMG,
        Rifle,
        SR,
        DMR,
        Special
    }
    public enum WeaponGrade
    {
        Normal,
        Special,
        Epic,
        Hero,
        Legendary,
        God
    }

    [Header("#Weapon Info")]
    public WeaponType type;
    public WeaponGrade grade;
    public string itemName;
    public float fireRate;
    public float maxBulletCount;
    public float damage;
    public int bulletShootCount;
    public float range;
    public float reloadingTime;
    public Sprite weaponImage;
    public int weaponLevel;
    public float weaponCount;
    public int weaponNum;
   

    //public AudioClip reloadSfx;
    //public AudioClip gunSound;

}
