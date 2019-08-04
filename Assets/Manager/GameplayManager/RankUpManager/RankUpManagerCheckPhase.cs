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
        Player player = PlayerManager.Instance.GetPlayer(id);

        int currentMana = player.m_Mana.currentMana;

        FieldBlockType type = block.m_RowType;

        for (int i = conditions.Count - 1; i >= 0; i--)
        {
            UnitData data = conditions[i].m_HighRankSpirit;

            //unique
            if (!player.CheckUniqueUnit(data))
                conditions.RemoveAt(i);
            //mana
            else if (data.m_Cost > currentMana)
                conditions.RemoveAt(i);
            //position
            else if ((type == FieldBlockType.Front && !data.CanBeFrontline) || (type == FieldBlockType.Back && !data.CanBeBackline))
                conditions.RemoveAt(i);
            //condition
            else if (!conditions[i].Check(data1, data2))
                conditions.RemoveAt(i);
        }

        return conditions;
    }

    
}
