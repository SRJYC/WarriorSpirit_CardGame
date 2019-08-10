using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPlayer
{
    namespace AIAction
    {
        public class RankUp
        {
            public static void Do(AIMove.RankUpOption option)
            {
                //Debug.Log("AI Rank Up Unit");

                RankUpManagerSummonPhase.Summon(option.data,option.block,option.spirit1,option.spirit2);
            }
        }
    }
}
