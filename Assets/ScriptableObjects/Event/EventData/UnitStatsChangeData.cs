using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event data which contains a unit, one of its stats, value before change, and value after change
/// </summary>
public class UnitStatsChangeData : GameEventData
{
    public Unit m_Unit;
    public UnitStatsProperty m_ChangedStats;
    public int m_BeforeChange;
    public int m_AfterChange;
    public int m_Delta { get { return m_AfterChange - m_BeforeChange; } }
}
