using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Component/Condition")]
public class ConditionComponent : ScriptableObject
{
    public float score;

    [Header("Must Satisfy All Condition")]
    public List<ConditionCheck> conditions;

    private List<AbilityInfo> abilityInfos = new List<AbilityInfo>();

    public void Reset()
    {
        abilityInfos.Clear();

    }

    public bool Check()
    {
        foreach(ConditionCheck condition in conditions)
        {
            if(!condition.Check(abilityInfos.ToArray()))
            {
                return false;
            }
        }
        return true;
    }

    public void Add(AbilityInfo info)
    {
        abilityInfos.Add(info);
    }
}
