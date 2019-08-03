using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effect/ApplyStatus/VariedStatesChangeStatus")]
public class ApplyVariedStatesChangeStatusEffect : Effect
{
    public StatsChangeStatus m_Status;

    [Header("Status Value")]
    public UnitStatsProperty m_property;
    public string m_description;

    public enum DefaultType
    {
        basic,
        minimum,
        maximum,
    }
    [Header("Default value")]
    public DefaultType m_type;
    public int m_Value;

    [Header("Value Get From Source")]
    public UnitStatsProperty sourceProperty;
    public bool negative = false;

    public override void TakeEffect(AbilityInfo[] info)
    {
        base.TakeEffect(info);

        int value = GetValue();

        StatsChangeStatus status = Instantiate(m_Status);
        InitStatus(status, value);

        TargetInfo targetInfo = GetAbilityInfo<TargetInfo>();
        foreach (Unit unit in targetInfo.m_Targets)
        {
            Apply(unit,status);
        }
    }

    private void InitStatus(StatsChangeStatus status, int value)
    {
        status.m_Property = m_property;

        int deltaValue = 0;
        switch(m_type)
        {
            case DefaultType.basic:
                deltaValue = value + m_Value;
                break;
            case DefaultType.maximum:
                deltaValue = value >= m_Value ? m_Value : value;
                break;
            case DefaultType.minimum:
                deltaValue = value <= m_Value ? m_Value : value;
                break;
        }

        string op = deltaValue >= 0 ? " +" : " ";
        status.m_Description = m_description + " " + m_property.ToString() + op + deltaValue;

        status.m_ChangedValue = deltaValue;
    }

    private int GetValue()
    {
        int value;

        SourceInfo sourceInfo = GetAbilityInfo<SourceInfo>();
        Unit source = sourceInfo.m_Source;
        value = source.m_Data.GetStat(sourceProperty);

        if (negative)
            value = -value;

        return value;
    }

    private void Apply(Unit unit, StatsChangeStatus status)
    {
        //Debug.Log("["+this + "] apply [" + m_Status + "] to [" + unit+"]");
        unit.m_Status.ApplyStatus(Instantiate(status));
    }
}
