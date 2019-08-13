using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Ability/Effect/ChangeStats")]
public class StatsChangeEffect : Effect
{
    [Header("Property")]
    public UnitStatsProperty m_Property;
    public bool m_Negative;

    [Header("Type")]
    [SerializeField] protected int m_MinValue = 1;
    [SerializeField] protected ChangeType m_ChangeTypeToSpirit = ChangeType.minimum;
    [SerializeField] protected ChangeType m_ChangeTypeToWarrior = ChangeType.minimum;

    [HideInInspector]public int m_SourceDelta;
    [HideInInspector]public int m_TargetDelta;
    public int m_modifer { get { return m_SourceDelta + m_TargetDelta; } }

    protected EffectData m_EventData;

    [Header("Event")]
    public UnitEvent m_BeforeActionEvent;
    public UnitEvent m_BeforeReceiveEffectEvent;
    //public UnitEvent m_AfterReceiveEffectEvent;

    /// <summary>
    /// They type of changing in stats
    /// </summary>
    protected enum ChangeType
    {
        none,//deal no damage
        minimum,//deal 1 damage
        power,//deal damage same as source unit's power
    }

    public override void TakeEffect(AbilityInfo[] info)
    {
        base.TakeEffect(info);

        //Get Info
        SourceInfo sourceInfo = GetAbilityInfo<SourceInfo>();
        TargetInfo targetInfo = GetAbilityInfo<TargetInfo>();

        CreateEventData(sourceInfo, targetInfo);

        TrigggerBeforeActionEvent(sourceInfo.m_Source);

        //get source power;
        int pow = sourceInfo.GetPower();

        foreach (Unit unit in targetInfo.m_Targets)
        {
            m_TargetDelta = 0;

            m_EventData.m_CurrentTarget = unit;
            TriggerBeforeReceiveEffectEvent(unit);

            int value = GetValue(unit, pow);
            if (value > 0)
                ChangeStats(unit, value);
            else
                ChangeStats(unit, 0);

            //TriggerAfterReceiveEffectEvent(unit);
        }
    }

    protected void ChangeStats(Unit target, int value)
    {
        int amount = value;
        if (m_Negative)
            amount = -value;

        target.m_Data.ChangeStats(m_Property, amount);
    }

    protected void TrigggerBeforeActionEvent(Unit unit)
    {
        if (m_BeforeActionEvent != null)
            m_BeforeActionEvent.Trigger(unit, m_EventData);
    }

    protected void TriggerBeforeReceiveEffectEvent(Unit unit)
    {
        if (m_BeforeReceiveEffectEvent != null)
            m_BeforeReceiveEffectEvent.Trigger(unit, m_EventData);
    }

    protected void CreateEventData(SourceInfo sourceInfo, TargetInfo targetInfo)
    {
        m_EventData = new EffectData();
        m_EventData.m_Source = sourceInfo.m_Source;
        m_EventData.m_Targets = targetInfo.m_Targets;
        m_EventData.m_TriggeredEffect = this;
    }

    protected int GetValue(Unit target, int power)
    {
        ChangeType dt;
        if (target.m_Data.IsWarrior)
            dt = m_ChangeTypeToWarrior;
        else
            dt = m_ChangeTypeToSpirit;

        int value = 0;

        switch (dt)
        {
            case ChangeType.none:
                return 0;
            case ChangeType.minimum:
                value = m_MinValue;
                break;
            case ChangeType.power:
                value = power;
                break;
        }

        

        value += m_modifer;

        return value;
    }
}
