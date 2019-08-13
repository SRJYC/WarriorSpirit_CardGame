using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameMechanic/Evanescent")]
public class Evanescent : UnitPropertyMechanic
{
    public UnitStatsProperty property = UnitStatsProperty.Evanescent;
    protected override void Trigger(GameEventData eventData)
    {
        SingleUnitData data = eventData.CastDataType<SingleUnitData>();
        if (data == null)
            return;

        Unit unit = data.m_Unit;

        int value = unit.m_Data.GetStat(property);
        if (value > 0)
        {
            int negative = unit.m_Data.GetStat(UnitStatsProperty.Negative);
            int dur = unit.m_Data.GetStat(UnitStatsProperty.DUR);

            if(negative <= 0 && dur > 0)
            {
                value = -value;
            }

            data.m_Unit.m_Data.ChangeStats(UnitStatsProperty.DUR, value);
        }
    }
}
