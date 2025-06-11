using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyPosChange : MonoBehaviour
{
    [SerializeField] private LayerMask armyLayer;

    private void ArmyClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,armyLayer))
        {
            if (hit.transform.CompareTag("Army"))
            {
                //드래그 위치 변경


            }
        }
    }
}
