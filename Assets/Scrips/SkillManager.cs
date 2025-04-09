using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    [SerializeField] private Skill[] skills;

    private void Awake()
    {
        if (instance != null) instance = this;
    }



}
