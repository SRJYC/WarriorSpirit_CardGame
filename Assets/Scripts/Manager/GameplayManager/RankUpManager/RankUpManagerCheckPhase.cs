using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankUpManagerCheckPhase
{
    public static List<RankUpCondition> GetAllConditions(UnitData data1, UnitData data2, FieldBlock block)
    {
        List<RankUpCondition> conditions = new List<RankUpCondition>();
        if (data1.m_Owner.m_PlayerID != data2.m_Owner.m_PlayerID)
            return conditions;

        //add all rank ups from Spirit 1
        conditions.AddRange(data1.m_RankUps);

        //add rank ups with sacrifice from Spirit 2
        foreach (RankUpCondition rankUp in data2.m_RankUps)
        {
            if (rankUp.m_Scarifice)
                conditions.Add(rankUp);
        }

        PlayerID id = data1.m_Owner.m_PlayerID;
        CheckMana(conditions,id);

        CheckPosition(conditions, block);

        CheckConditions(conditions, data1, data2);

        return conditions;
    }

    private static void CheckMana(List<RankUpCondition> conditions, PlayerID id)
    {
        int currentMana = PlayerManager.Instance.GetPlayer(id).m_Mana.currentMana;

        for (int i = conditions.Count - 1; i >= 0; i--)
        {
            UnitData data = conditions[i].m_HighRankSpirit;

            if (data.m_Cost > currentMana)
                conditions.RemoveAt(i);
        }
    }

    private static void CheckPosition(List<RankUpCondition> conditions, FieldBlock block)
    {
        FieldBlockType type = block.m_RowType;

        for (int i = conditions.Count - 1; i >= 0; i--)
        {
            UnitData data = conditions[i].m_HighRankSpirit;

            //can't summoned on block
            if ((type == FieldBlockType.Front && !data.CanBeFrontline) || (type == FieldBlockType.Back && !data.CanBeBackline))
                conditions.RemoveAt(i);
        }
    }

    private static void CheckConditions(List<RankUpCondition> conditions, UnitData data1, UnitData data2)
    {
        for (int i = conditions.Count - 1; i >= 0; i--)
        {
            if (!conditions[i].Check(data1, data2))
                conditions.RemoveAt(i);
        }
    }

    
}
