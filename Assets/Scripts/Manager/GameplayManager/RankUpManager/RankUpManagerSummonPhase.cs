using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankUpManagerSummonPhase
{
    public static void Summon(GameObject selectedCard, FieldBlock block, UnitData data1, UnitData data2)
    {
        PlayerID id = PlayerID.Player1;
        if (data1.m_Owner != null)
        {
            id = data1.m_Owner.m_PlayerID;
            data1.m_Owner.m_Destroy.Destroy();
        }
        if (data2.m_Owner != null)
        {
            id = data1.m_Owner.m_PlayerID;
            data2.m_Owner.m_Destroy.Destroy();
        }

        UnitData data = selectedCard.GetComponent<CardDisplay>().m_CardData;

        CostMana(id, data);

        SummonManager.Summon(data, block, id);
    }

    private static void CostMana(PlayerID id, UnitData data)
    {
        int cost = data.m_Cost;
        ManaObject mana = PlayerManager.Instance.GetPlayer(id).m_Mana;
        mana.Cost(cost);
    }
}
