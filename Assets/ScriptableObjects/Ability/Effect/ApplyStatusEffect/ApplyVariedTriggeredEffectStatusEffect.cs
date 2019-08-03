using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effect/ApplyStatus/VariedTriggeredEffectStatus")]
public class ApplyVariedTriggeredEffectStatusEffect : Effect
{
    public TriggeredEffectStatus m_Status;

    public enum DefaultType
    {
        constant,
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

        int value = 0;
        if (m_type != DefaultType.constant)
        {
            value = GetValue();
        }

        TriggeredEffectStatus status = m_Status;
        InitStatus(status, value);

        TargetInfo targetInfo = GetAbilityInfo<TargetInfo>();
        foreach (Unit unit in targetInfo.m_Targets)
        {
            Apply(unit, status);
        }
    }

    private void InitStatus(TriggeredEffectStatus status, int value)
    {
        int deltaValue = 0;
        switch (m_type)
        {
            case DefaultType.constant:
                deltaValue = m_Value;
                break;
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

        status.power = deltaValue;
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

    private void Apply(Unit unit, Status status)
    {
        //Debug.Log("["+this + "] apply [" + m_Status + "] to [" + unit+"]");
        unit.m_Status.ApplyStatus(Instantiate(status));
    }
}

