using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro damageMesh;
    private float dissapearTime = 0.5f;

    private void Awake()
    {
        damageMesh = GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        dissapearTime -= Time.deltaTime;
        if (dissapearTime <= 0.0f) Destroy(this.gameObject);
    }
    public static DamagePopUp Create(Vector3 pos, float damage)
    {
        Transform damagePopUpTransform = Instantiate(GameAsset.Instance.pfDamagePopUp,
            pos, Quaternion.identity);
        DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.SetUp(damage);

        return damagePopUp;
    }
    public void SetUp(float damage)
    {
        damageMesh.SetText(Mathf.FloorToInt(damage).ToString());
    }


}
