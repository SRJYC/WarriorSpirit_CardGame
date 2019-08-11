using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraManager : Singleton<AuraManager>
{
    [SerializeField] private List<Aura> m_AuraList;

    [SerializeField] private List<Aura> m_ToRemove;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        m_AuraList = new List<Aura>();
        m_ToRemove = new List<Aura>();
    }

    public void AddAura(Aura aura)
    {
        m_AuraList.Add(aura);
    }

    public void RemoveAura(Aura aura)
    {
        m_ToRemove.Add(aura);
    }

    public void RefreshAura(List<Unit> units)
    {
        foreach(Aura aura in m_AuraList)
        {
            if(m_ToRemove.Contains(aura))
            {
                aura.RemoveAura(units);
            }
            else
            {
                aura.RefreshAura(units);
            }
        }

        ActualRemoveAura();
    }

    public void RemoveUnit(Unit unit)
    {
        foreach (Aura aura in m_AuraList)
        {
            aura.RemoveUnit(unit);
        }
    }

    private void ActualRemoveAura()
    {
        foreach(Aura aura in m_ToRemove)
        {
            m_AuraList.Remove(aura);
        }
        m_ToRemove.Clear();
    }
}
