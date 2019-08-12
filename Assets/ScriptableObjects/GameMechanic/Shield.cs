using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameMechanic/Shield")]
public class Shield : UnitPropertyMechanic
{
    public UnitStatsProperty property = UnitStatsProperty.Shield;
    protected override void Trigger(GameEventData eventData)
    {
        EffectData data = eventData.CastDataType<EffectData>();
        if (data == null)
            return;

        int value = data.m_CurrentTarget.m_Data.GetStat(property);
        if (value > 0)
        {
            data.m_TriggeredEffect.m_TargetDelta -= value;
        }
    }
}
