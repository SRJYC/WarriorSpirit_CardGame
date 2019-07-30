using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonManager
{
    public static void SummonWarrior(PlayerID id, UnitData data)
    {
        FieldBlock block = BoardManager.Instance.GetFieldController(id).GetBlockByPosition(FieldBlockType.Center,FieldBlockType.Middle);

        Summon(data, block, id);
    }
    public static void Summon(UnitData data, FieldBlock block, PlayerID id)
    {
        GameObject card = DeckManager.Instance.GetCard(data, id);
        Unit unit = card.GetComponent<Unit>();

        Summon(unit, block);
    }

    public static void Summon(Unit unit, FieldBlock block)
    {
        unit.m_Position.MoveTo(block);
        unit.GetComponent<CardDraggable>().Disable();

        OrderManager.Instance.AddUnit(unit);
    }

    public static bool CheckPosition(Unit unit, FieldBlock block)
    {
        return CheckPosition(unit.m_Data, block);
    }

    public static bool CheckPosition(UnitData data, FieldBlock block)
    {
        if (block.m_Unit != null)
            return false;

        FieldBlockType type = block.m_RowType;

        if (type == FieldBlockType.Front && !data.CanBeFrontline)
            return false;
        else if (type == FieldBlockType.Back && !data.CanBeBackline)
            return false;
        else if (type == FieldBlockType.Center && !data.IsWarrior)
            return false;

        return true;
    }
    public static bool CheckCost(Unit unit, ManaObject mana)
    {
        int cost = unit.m_Data.GetStat(UnitStatsProperty.Cost);
        return cost <= mana.currentMana;
    }
}
