using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AIPlayer
{
    namespace AIRule
    {
        public class EvaluateAbility
        {
            public const int cdScore = -2;
            public const int manaCostScore = -4;
            public const int effectMultiplier = 5;
            public const int conditionMultiplier = -5;
            public static float Evaluate(Ability ability)
            {
                float score = 0;

                score += ability.m_CD * cdScore;
                score += ability.m_ManaCost * manaCostScore;

                foreach(ConditionComponent condition in ability.m_Conditions)
                {
                    score += condition.score * conditionMultiplier;
                }
                foreach (EffectComponent effect in ability.m_Effects)
                {
                    score += effect.score * effectMultiplier;
                }

                return score;
            }
        }
    }
}
