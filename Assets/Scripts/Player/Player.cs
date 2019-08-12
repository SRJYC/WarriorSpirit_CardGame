using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerID m_ID { get; private set; }

    public Deck m_Deck;
    public Hand m_Hand;
    public ManaObject m_Mana;
    public MyUnits m_UnitsOnBoard;

    public ManaDisplay manaDisplay;

    [Header("Event")]
    public GameEvent m_SummonEvent;

    private void Start()
    {
        m_Deck = GetComponentInChildren<Deck>();
        m_Hand = GetComponentInChildren<Hand>();
        m_UnitsOnBoard = GetComponent<MyUnits>();
        m_UnitsOnBoard.Init(this);

        Register();
    }

    private void OnDestroy()
    {
        Unregister();
    }

    public virtual void Init(PlayerID id)
    {
        m_ID = id;

        CardCollection deck = GameData.Instance.GetDeck(m_ID);
        m_Deck.Init(m_Hand, deck, m_ID);

        m_Mana = Instantiate(m_Mana);
        manaDisplay.Init(m_Mana);

        //summon warrior
        SummonManager.SummonWarrior(m_ID,deck.m_Warrior);
    }

    protected virtual void Register()
    {
        if(m_SummonEvent != null)
            m_SummonEvent.RegisterListenner(TrySummon);
    }

    protected virtual void Unregister()
    {
        if (m_SummonEvent != null)
            m_SummonEvent.UnregisterListenner(TrySummon);
    }

    protected virtual void TrySummon(GameEventData eventData)
    {
        SummonData data = eventData.CastDataType<SummonData>();
        if (data == null)
            return;

        Unit unit = data.m_Unit;
        FieldBlock block = data.m_Block;

        if (!CheckSummonAvaliability(data, unit, block))
            return;

        int cost = unit.m_Data.GetStat(UnitStatsProperty.Cost);
        m_Mana.Cost(cost);

        SummonManager.Summon(unit, block);
    }

    private bool CheckSummonAvaliability(SummonData data, Unit unit, FieldBlock block)
    {
        //check id
        if (data.m_Block.m_PlayerID != m_ID)
        {
            return false;
        }

        //check position
        if (!SummonManager.CheckPosition(unit, block))
        {
            GameMessage.Instance.Display("That Spirit can't be summoned on that position.");
            return false;
        }


        //check unique
        if (!CheckUniqueUnit(unit.m_Data))
        {
            GameMessage.Instance.Display("There can only be one Unique Spirit on Battlefield.");
            return false;
        }

        Debug.Log("Test Mark");
        
        //check cost
        if (!PlayerManager.Instance.test && !SummonManager.CheckCost(unit, m_Mana))
        {
            GameMessage.Instance.Display("You don't have enough Mana");
            return false;
        }

        return true;
    }

    public bool CheckUniqueUnit(UnitData unit)
    {
        if (unit.IsUnique && m_UnitsOnBoard.SameUnitExist(unit))
            return false;

        return true;
    }
}
