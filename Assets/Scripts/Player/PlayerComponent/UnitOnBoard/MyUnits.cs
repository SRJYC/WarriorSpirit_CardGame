using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUnits : MonoBehaviour
{
    public List<Unit> m_Units;

    public GameEvent m_TurnStartEvent;

    private void Start()
    {
        m_TurnStartEvent.RegisterListenner(ReduceCooldown);
    }

    private void OnDestroy()
    {
        m_TurnStartEvent.UnregisterListenner(ReduceCooldown);
    }

    public bool SameUnitExist(UnitData data)
    {
        string name = data.UnitName;

        foreach(Unit unit in m_Units)
        {
            if (unit.m_Data.UnitName == name)
                return true;
        }
        return false;
    }

    public void SummonToBoard(Unit unit)
    {
        m_Units.Add(unit);
    }

    public void RemoveFromBoard(Unit unit)
    {
        m_Units.Remove(unit);
    }

    public void ReduceCooldown(GameEventData eventData)
    {
        foreach(Unit unit in m_Units)
        {
            foreach(Ability ability in unit.m_Data.m_Abilities)
            {
                if(ability.m_CurrentCD > 0)
                    ability.m_CurrentCD--;
            }
        }
    }
}
