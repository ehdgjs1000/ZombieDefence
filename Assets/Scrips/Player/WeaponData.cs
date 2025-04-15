using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("#Weapon Info")]
    public WeaponType type;
    public string itemName;
    public float fireRate;
    public float maxBulletCount;
    public float damage;
    public int bulletShootCount;
    public float range;
    public float reloadingTime;

    //public AudioClip reloadSfx;
    //public AudioClip gunSound;

}
