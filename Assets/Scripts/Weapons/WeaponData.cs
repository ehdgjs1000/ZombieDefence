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
    public WeaponType type;         // #0
    public WeaponGrade grade;       // #1
    public string itemName;         // #2
    public float fireRate;          // #3
    public float maxBulletCount;    // #4
    public float damage;            // #5
    public int bulletShootCount;    // #6
    public float range;             // #7
    public float reloadingTime;     // #8
    public Sprite weaponImage;      // #9
    public int weaponLevel;         // #10
    public float weaponCount;       // #11
    public int weaponNum;           // #12


    //public AudioClip reloadSfx;
    //public AudioClip gunSound;

}
