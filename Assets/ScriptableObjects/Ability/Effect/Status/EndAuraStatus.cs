using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Status/EndAura")]
public class EndAuraStatus : Status
{
    public Aura m_Aura;

    public UnitEvent m_EndAuraEvent;

    protected override void Regiseter()
    {
        //Debug.Log(this + " End Aura Effect Register");
        m_EndAuraEvent.RegisterListenner(m_Status.m_Owner,EndAura);
        //m_EndAuraEvent.Trigger(m_Status.m_Owner);
    }

    protected override void Unregister()
    {
        //Debug.Log("End Aura Effect UnRegister");
        m_EndAuraEvent.UnregisterListenner(m_Status.m_Owner, EndAura);
    }

    public void EndAura(GameEventData data)
    {
        //Debug.Log("End Aura Effect Triggered");
        AuraManager.Instance.RemoveAura(m_Aura);
        m_Status.RemoveStatus(this);
    }
}
