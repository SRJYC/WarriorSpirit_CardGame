using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : ScriptableObject
{
    public UnitStatsProperty[] unitStats;
    public int[] valueForStats;

    public Dictionary<UnitStatsProperty, int> m_Stats;

    public UnitStats clone()
    {
        return Instantiate(this);
    }
}
