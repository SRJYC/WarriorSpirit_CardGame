using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPlayer
{
    namespace AIAction
    {
        public class Summon
        {
            public static void Do(AIMove.UnitOption option)
            {
                Debug.Log("AI Summon Unit");

                UnitData data = option.data;

                if(AIView.Instance.EmptyFront != null && data.CanBeFrontline)
                {
                    int cost = data.GetStat(UnitStatsProperty.Cost);
                    AIView.Instance.AI.m_Mana.Cost(cost);

                    SummonManager.Summon(data.m_Owner, AIView.Instance.EmptyFront);
                }
                else if (AIView.Instance.EmptyBack != null && data.CanBeBackline)
                {
                    int cost = data.GetStat(UnitStatsProperty.Cost);
                    AIView.Instance.AI.m_Mana.Cost(cost);

                    SummonManager.Summon(data.m_Owner, AIView.Instance.EmptyBack);
                }
            }
        }
    }
}
