using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Status/StatusEffect/ModifyAbility")]
public class ModifyAbility : StatusEffect
{
    public override void TakeEffect(Unit unit, int power = 0, GameEventData eventData = null)
    {
        base.TakeEffect(unit, power, eventData);

        EffectData data = eventData.CastDataType<EffectData>();
        if (data == null)
            return;

        data.m_TriggeredEffect.m_TargetDelta += value;
    }
}
