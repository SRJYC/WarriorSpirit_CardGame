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

            public static float Evaulate(UnitData data)
            {
                float score = 0;

                score += data.Rank * scorePerRank;
                score += data.m_Durability * scorePerDUR;
                score += data.m_Power * scorePerPOW;
                score += data.m_Speed * scorePerSPD;
                score += data.m_Cost * scorePerCost;
                //Debug.Log("\t Unit Stats Evaulate Score: [" + score + "]");

                foreach (Ability ability in data.m_Abilities)
                {
                    score += EvaluateAbility.Evaluate(ability);
                }
                //Debug.Log("\t Unit Ability Evaulate Score: [" + score + "]");

                return score;
            }
        }
    }
}
