using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUnits : MonoBehaviour
{
    //when player has summoned 2 copies of new Spirits on his field, the Spirit will be added to the deck, so he can draw it diretly later.
    public const int NumberOfCopyToAdd = 1; 

    public List<Unit> m_Units;
    public GameEvent m_TurnStartEvent;
    public Dictionary<UnitData, int> m_History;

    [HideInInspector] public Player m_Player;
    public void Init(Player player)
    {
        m_Player = player;
        m_Units = new List<Unit>();
        m_History = new Dictionary<UnitData, int>();
    }

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
        AddHistory(unit);
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

    public void AddHistory(Unit unit)
    {
        UnitData origin = unit.m_Data.m_OriginData;

        int value = IncreaseNumber(origin);

        if (value >= NumberOfCopyToAdd)
        {
            m_Player.m_Deck.AddToDeck(origin);
        }
    }

    private int IncreaseNumber(UnitData origin)
    {
        if (origin.IsWarrior)
            return -1;

        int value = 0;
        if (m_History.TryGetValue(origin, out value))
        {
            return ++m_History[origin];
        }
        else
        {
            m_History.Add(origin, 1);
            return 1;
        }
    }

    public int GetHistoryNumber(UnitData data)
    {
        UnitData origin = data;
        if (data.m_OriginData != null)
            origin = data.m_OriginData;

        int value = 0;
        m_History.TryGetValue(origin, out value);
        return value;
    }
}
