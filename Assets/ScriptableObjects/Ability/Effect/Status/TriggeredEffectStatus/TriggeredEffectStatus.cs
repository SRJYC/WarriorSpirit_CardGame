using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Ability/Status/TriggeredEffect")]
public class TriggeredEffectStatus : Status
{
    [Header("Varible")]
    public int power;

    [Header("Triggered Event (can only be one and can't be same as End Event)")]
    public bool isGameEvent;
    public GameEvent gameEvent;
    public bool isUnitEvent;
    public UnitEvent unitEvent;

    [Header("Triggered Effect")]
    public List<StatusEffect> effects;

    [Header("Effect At End (can by itself or with other event)")]
    public bool triggeredAtEnd;

    [Header("Triggered Effect")]
    public List<StatusEffect> endEffects;

    public override void AffectUnit(UnitStatus unitStatus)
    {
        base.AffectUnit(unitStatus);

    }

    public override void EndAffect()
    {
        base.EndAffect();

    }

    protected override void Regiseter()
    {
        if(m_EndEffectEvent != null)
            m_EndEffectEvent.RegisterListenner(End);

        if (isGameEvent)
            gameEvent.RegisterListenner(TakeEffect);
        else if(isUnitEvent)
            unitEvent.RegisterListenner(m_Status.m_Owner, TakeEffect);
    }

    protected override void Unregister()
    {
        if (m_EndEffectEvent != null)
            m_EndEffectEvent.UnregisterListenner(End);

        if (isGameEvent)
            gameEvent.UnregisterListenner(TakeEffect);
        else if(isUnitEvent)
            unitEvent.UnregisterListenner(m_Status.m_Owner, TakeEffect);
    }

    private void End(GameEventData data = null)
    {
        m_Duration--;
        if (m_Duration <= 0)
        {
            TriggerEndEffects();

            m_Status.RemoveStatus(this);
        }
    }

    private void TriggerEndEffects()
    {
        if (triggeredAtEnd)
        {
            foreach (StatusEffect effect in endEffects)
            {
                effect.TakeEffect(m_Status.m_Owner, power);
            }
        }
    }

    private void TakeEffect(GameEventData data)
    {
        foreach(StatusEffect effect in effects)
        {
            effect.TakeEffect(m_Status.m_Owner, power, data);
        }
    }
}
