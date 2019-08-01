using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPlayer
{
    namespace AIRule
    {
        public class EvaluateUnitData
        {
            public const int scorePerRank = 15;
            public const int scorePerDUR = 8;
            public const int scorePerPOW = 3;
            public const int scorePerSPD = 3;
            public const int scorePerCost = -10;

            public static int Evaulate(UnitData data)
            {
                int score = 0;

                score += data.Rank * scorePerRank;
                score += data.m_Durability * scorePerDUR;
                score += data.m_Power * scorePerPOW;
                score += data.m_Speed * scorePerSPD;
                score += data.m_Cost * scorePerCost;

                foreach(Ability ability in data.m_Abilities)
                {
                    score += EvaluateAbility.Evaluate(ability);
                }

                return score;
            }
        }
    }
}
