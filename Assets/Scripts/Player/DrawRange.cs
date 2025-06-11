using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRange : MonoBehaviour
{
    public LineRenderer line;
    private float range;
    private int subdivisions = 60;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        range = this.GetComponent<Army>().attackRange;
    }
    private void Update()
    {
        DrawAttackRange();
    }
    private void DrawAttackRange()
    {
        float angleStep = 2f * Mathf.PI / subdivisions;

        line.positionCount = subdivisions;

        for (int i = 0; i < subdivisions; i++)
        {
            float x = range * Mathf.Cos(angleStep * i);
            float z = range * Mathf.Sin(angleStep * i);

            Vector3 pointInCircle = new Vector3(transform.position.x + x,0.5f
                , transform.position.z + z);

            if(this.GetComponent<Army>().ReturnEnemyCount() > 0)
            {
                line.SetColors(Color.red, Color.red);
            }
            else
            {
                line.SetColors(Color.green, Color.green);
            }


            line.SetPosition(i,pointInCircle);
        }
    }

}
