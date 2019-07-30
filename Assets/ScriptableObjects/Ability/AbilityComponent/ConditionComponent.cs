using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Component/Condition")]
public class ConditionComponent : ScriptableObject
{
    public ConditionCheck condition;

    private List<AbilityInfo> abilityInfos = new List<AbilityInfo>();

    public void Reset()
    {
        abilityInfos.Clear();

    }

    public bool Check()
    {
        return condition.Check(abilityInfos.ToArray());
    }

    public void Add(AbilityInfo info)
    {
        abilityInfos.Add(info);
    }
}
