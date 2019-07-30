using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatus
{
    public readonly Unit m_Owner;

    private List<Status> m_Status;

    public List<SpiritType> m_SpiritTypes;

    private Dictionary<UnitStatsProperty, int> m_Stats;

    public UnitStatus(Unit unit)
    {
        m_Owner = unit;
        m_Status = new List<Status>();

        m_Stats = new Dictionary<UnitStatsProperty, int>();
        m_SpiritTypes = new List<SpiritType>();
    }

    public void ApplyStatus(Status status)
    {
        m_Status.Add(status);
        status.AffectUnit(this);
    }

    public void RemoveStatus(Status status)
    {
        m_Status.Remove(status);
        status.EndAffect();
    }

    public List<Status> GetAllStatus()
    {
        return m_Status;
    }

    public int GetStat(UnitStatsProperty stat)
    {
        int value;
        m_Stats.TryGetValue(stat, out value);
        //Debug.Log(m_Owner + " has Status: [" + stat + ":" + value + "]");
        return value;
    }

    public void ChangeStats(UnitStatsProperty stat, int amount, bool set = false, bool force = false)
    {
        if (stat == UnitStatsProperty.DUR)
            Debug.LogError("Status should not change Durability");

        if (m_Stats.ContainsKey(stat))
        {
            if (set)
            {
                m_Stats[stat] = amount;
            }
            else
            {
                m_Stats[stat] += amount;
            }
        }
        else if (force)
        {
            m_Stats.Add(stat, amount);
        }
        else
            Debug.LogError("Can't Change " + stat + ": Stats Doesn't Exist");
    }

    public Dictionary<UnitStatsProperty, int> GetAllStat()
    {
        return m_Stats;
    }
}
