using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effect/ApplyStatus/ConstantStatus")]
public class ApplyConstantStatusEffect : Effect
{
    public Status m_Status;
    public override void TakeEffect(AbilityInfo[] info)
    {
        base.TakeEffect(info);

        TargetInfo targetInfo = GetAbilityInfo<TargetInfo>();
        foreach (Unit unit in targetInfo.m_Targets)
        {
            Apply(unit);
        }
    }

    private void Apply(Unit unit)
    {
        //Debug.Log("["+this + "] apply [" + m_Status + "] to [" + unit+"]");
        unit.m_Status.ApplyStatus(Instantiate(m_Status));
    }
}
