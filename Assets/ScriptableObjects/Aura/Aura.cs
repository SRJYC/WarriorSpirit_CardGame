using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Aura/Aura")]
public class Aura : ScriptableObject
{
    [Header("Range")]
    public List<TargetInfoGetter> m_InfoGetter;
    private List<FieldBlock> m_EffectArea;

    [Header("Condition For Unit")]
    public SingleUnitCondition m_Condition;

    [Header("Status To Apply")]
    public Status m_Status;

    private Dictionary<Unit, Status> m_StatusRecord;

    public void Init(Unit source)
    {
        GetArea(source);
        m_StatusRecord = new Dictionary<Unit, Status>();
    }

    private void GetArea(Unit source)
    {
        m_EffectArea = new List<FieldBlock>();
        foreach (FixedTargetInfoGetter infoGetter in m_InfoGetter)
        {
            infoGetter.GetInfo(source);
            m_EffectArea.AddRange(infoGetter.m_Blocks);
        }
    }

    public void RemoveUnit(Unit unit)
    {
        m_StatusRecord.Remove(unit);
    }

    public void RefreshAura(List<Unit> units)
    {
        foreach(Unit unit in units)
        {
            if (CheckInArea(unit))
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

    private bool CheckInArea(Unit unit)
    {
        FieldBlock block = unit.m_Position.m_Block;
        return m_EffectArea.Contains(block);
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
