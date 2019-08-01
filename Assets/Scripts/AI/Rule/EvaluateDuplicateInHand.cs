using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  AIPlayer
{
    namespace AIRule
    {
        public class EvaluateDuplicateInHand
        {
            public const int scorePerDuplicate = -20;

            public static int Evaulate(UnitData data)
            {
                string name = data.UnitName;

                int score = 0;

                List<Unit> cards = AIView.Instance.CardsInHand;
                foreach (Unit unit in cards)
                {
                    if (unit.m_Data.UnitName == name)
                        score += scorePerDuplicate;
                }

                return score;
            }
        }
    }
}