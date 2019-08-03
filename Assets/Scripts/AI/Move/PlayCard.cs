using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPlayer
{
    namespace AIMove
    {
        public class PlayCard
        {
            //score ratio for Spirits that's used in Rank up, need subtract from it
            public const float rankUpScoreRatio = 0.07f;

            public static void Think()
            {
                if (AIView.Instance.Mana == 0)
                {
                    AIAction.Pass.Do();
                    return;
                }

                //get all possible options
                List<Unit> cards = AIView.Instance.CardsInHand;
                List<Unit> spiritsOnBoard = AIView.Instance.AllAllySpirits;
                
                //evaluate summon
                List<UnitOption> options = EvaluateSummon(cards);

                //evaluate rankup
                List<RankUpOption> upOptions = EvaluateRankUp(cards, spiritsOnBoard);

                //all
                List<Option> list = new List<Option>();
                list.AddRange(options);
                list.AddRange(upOptions);

                if(list.Count == 0)
                {
                    AIAction.Pass.Do();
                    return;
                }

                Option op = Common.RandomOptionOfBest<Option>(list);

                if(op.GetType() == typeof(UnitOption))
                {
                    AIAction.Summon.Do((UnitOption)op);
                }
                else if(op.GetType() == typeof(RankUpOption))
                {
                    AIAction.RankUp.Do((RankUpOption)op);
                }

            }

            private static List<UnitOption> EvaluateSummon(List<Unit> cards)
            {
                List<UnitOption> list = new List<UnitOption>();

                //check position
                FieldBlock emptyfront = AIView.Instance.EmptyFront;
                FieldBlock emptyback = AIView.Instance.EmptyBack;
                if (emptyfront == null && emptyback == null)
                    return list;//no position to summon

                for (int i = cards.Count - 1; i>=0; i--)
                {
                    UnitData data = cards[i].m_Data;

                    //check mana
                    int mana = AIView.Instance.Mana;
                    if (data.GetStat(UnitStatsProperty.Cost) > mana)
                        continue;

                    //check position
                    bool canbefront = emptyfront != null && data.CanBeFrontline;
                    bool canbeback = emptyback != null && data.CanBeBackline;
                    if (!canbefront && !canbeback)
                        continue;

                    //check unique
                    if (data.IsUnique && AIView.Instance.AI.m_UnitsOnBoard.SameUnitExist(data))
                        continue;

                    //evaluate
                    Debug.Log("Summon Option [" + data.UnitName + "]");

                    UnitOption option = new UnitOption(data);

                    float score = AIRule.EvaluateUnitData.Evaulate(data);
                    option.score += score;
                    Debug.Log("\t Unit Evaulate Score: [" + score + "]");

                    Debug.Log("Total: " + option.score);

                    list.Add(option);
                }

                return list;
            }

            private static List<RankUpOption> EvaluateRankUp(List<Unit> hand, List<Unit> board)
            {
                List<RankUpOption> list = new List<RankUpOption>();

                foreach(Unit unit1 in board)
                {
                    FieldBlock block = unit1.m_Position.m_Block;

                    foreach(Unit unit2 in hand)
                    {
                        List<RankUpCondition> rankup = RankUpManagerCheckPhase.GetAllConditions(unit1.m_Data, unit2.m_Data, block);

                        foreach(RankUpCondition ruc in rankup)
                        {
                            UnitData data = ruc.m_HighRankSpirit;

                            //evaluate
                            Debug.Log("Rank Up Option: [" + unit1.m_Data.UnitName + "](board) + [" + unit2.m_Data.UnitName + "](hand) => [" + data.UnitName + "]");

                            RankUpOption option = new RankUpOption(data, unit1.m_Data, unit2.m_Data, block);

                            float score = AIRule.EvaluateUnitData.Evaulate(data);
                            option.score += score;
                            Debug.Log("\t Unit Evaulate Score: [" + score + "]");

                            float scoreForUnitOnBoard = AIRule.EvaluateUnitData.Evaulate(unit1.m_Data);
                            scoreForUnitOnBoard = scoreForUnitOnBoard * rankUpScoreRatio;
                            option.score -= scoreForUnitOnBoard;
                            Debug.Log("\t ["+ unit1.m_Data.UnitName + "] Unit Evaulate Score: (-)[" + scoreForUnitOnBoard + "]");

                            float scoreForUnitOnHand = AIRule.EvaluateUnitData.Evaulate(unit2.m_Data);
                            scoreForUnitOnHand = scoreForUnitOnHand * rankUpScoreRatio;
                            option.score -= scoreForUnitOnHand;
                            Debug.Log("\t [" + unit2.m_Data.UnitName + "] Unit Evaulate Score: (-)[" + scoreForUnitOnHand + "]");

                            Debug.Log("Total: " + option.score);

                            list.Add(option);
                        }
                    }
                }
                return list;
            }
        }
    }
}