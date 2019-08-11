using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Status/StatusEffect/ChangeStats")]
public class ChangeStats : StatusEffect
{
    public UnitStatsProperty property;
    public bool force;
    public override void TakeEffect(Unit unit, int power = 0, GameEventData eventData = null)
    {
        base.TakeEffect(unit, power);

        unit.m_Data.ChangeStats(property,value,false,force);
    }
}
