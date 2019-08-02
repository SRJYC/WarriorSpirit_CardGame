using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPlayer
{
    namespace AIRule
    {
        public class EvaluateTarget
        {
            public static int Evaluate(FieldBlock block, Ability ability)
            {
                bool postive = ability.AIPositive;
                bool ally = block.m_PlayerID == ability.m_Owner.m_PlayerID;
                bool xor = postive ^ ally;

                if (!xor)
                    return 100;
                else
                    return 0;
            }
        }
    }
}
