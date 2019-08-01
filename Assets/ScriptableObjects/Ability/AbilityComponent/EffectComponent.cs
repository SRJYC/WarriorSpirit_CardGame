using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Ability/Component/Effect")]
public class EffectComponent : ScriptableObject
{
    public float score;

    public Effect effect;
    public Effect alertEffect;

    private List<AbilityInfo> abilityInfos = new List<AbilityInfo>();
    private List<ConditionComponent> conditions = new List<ConditionComponent>();

    public void Reset()
    {
        abilityInfos.Clear();
        conditions.Clear();
    }

    public void Add(AbilityInfo info)
    {
        abilityInfos.Add(info);
    }

    public void Add(ConditionComponent condition)
    {
        conditions.Add(condition);
    }

    public void TakeEffect()
    {
        //Debug.Log(this + " take effect with condition " + CheckCondition());
        if (CheckCondition())
            effect.TakeEffect(abilityInfos.ToArray());
        else if (alertEffect != null)
            alertEffect.TakeEffect(abilityInfos.ToArray());
    }

    private bool CheckCondition()
    {
        foreach(ConditionComponent condition in conditions)
        {
            if (!condition.Check())
                return false;
        }
        return true;
    }
}
