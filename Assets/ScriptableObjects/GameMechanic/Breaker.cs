using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameMechanic/Breaker")]
public class Breaker : UnitPropertyMechanic
{
    public UnitStatsProperty property = UnitStatsProperty.Breaker;
    protected override void Trigger(GameEventData eventData)
    {
        EffectData data = eventData.CastDataType<EffectData>();
        if (data == null)
            return;

        int breaker = data.m_Source.m_Data.GetStat(property);
        if (breaker > 0)
        {
            data.m_TriggeredEffect.m_SourceDelta += breaker;
        }
    }
}
