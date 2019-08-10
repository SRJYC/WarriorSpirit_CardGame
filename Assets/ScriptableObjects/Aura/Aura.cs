using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Aura/Aura")]
public class Aura : ScriptableObject
{
    [Header("Spirit or Warrior")]
    public bool checkWarrior = true;
    public bool checkSpirit = true;

    [Header("Ally or Enemy")]
    [HideInInspector] public PlayerID m_ID;
    public bool checkAlly;
    public bool checkEnemy;

    [Header("Condition For Unit")]
    public SingleUnitCondition m_Condition;

    [Header("Status To Apply")]
    public Status m_Status;

    private Dictionary<Unit, Status> m_StatusRecord;

    public void Init(PlayerID id)
    {
        m_ID = id;
        m_StatusRecord = new Dictionary<Unit, Status>();
    }

    public void RemoveUnit(Unit unit)
    {
        m_StatusRecord.Remove(unit);
    }

    public void RefreshAura(List<Unit> units)
    {
        foreach(Unit unit in units)
        {
            if (GetRightSide(unit) && GetRightType(unit))
            {
                bool result = m_Condition.Check(unit.m_Data);

                if (result)
                {
                    TryApplyStatus(unit);
                }
                else
                {
                    TryRemoveStatus(unit);
                }
            }
        }
    }

    private bool GetRightType(Unit unit)
    {
        bool warrior = unit.m_Data.IsWarrior;
        bool rightType = (warrior && checkWarrior) || (!warrior && checkSpirit);
        return rightType;
    }

    private bool GetRightSide(Unit unit)
    {
        PlayerID id = unit.m_PlayerID;
        bool ally = id == m_ID;
        bool rightSide = (ally && checkAlly) || (!ally && checkEnemy);
        return rightSide;
    }

    public void RemoveAura(List<Unit> units)
    {
        foreach (Unit unit in units)
        {
            TryRemoveStatus(unit);
        }
    }

    private void TryRemoveStatus(Unit unit)
    {
        Status status;
        if (m_StatusRecord.TryGetValue(unit, out status))
        {
            unit.m_Status.RemoveStatus(status);
        }
    }

    private void TryApplyStatus(Unit unit)
    {
        if (!m_StatusRecord.ContainsKey(unit))
        {
            Status status = Instantiate(m_Status);
            unit.m_Status.ApplyStatus(status);

            m_StatusRecord.Add(unit, status);
        }
    }
}
