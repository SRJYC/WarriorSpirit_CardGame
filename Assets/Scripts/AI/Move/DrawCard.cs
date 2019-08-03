using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPlayer
{
    namespace AIMove
    {
        public class DrawCard
        {
            public static UnitData Think(List<UnitData> units)
            {
                List<UnitOption> options = Evaluate(units);

                UnitOption op = Common.RandomOptionOfBest<UnitOption>(options);

                return op.data;
            }

            private static List<UnitOption> Evaluate(List<UnitData> options)
            {
                List<UnitOption> list = new List<UnitOption>();

                //Debug.Log("AI Draw Card");
                foreach (UnitData op in options)
                {
                    //Debug.Log("Option [" + op.UnitName + "]");

                    UnitOption option = new UnitOption(op);

                    float score = AIRule.EvaluateUnitData.Evaulate(op);
                    option.score += score;
                    //Debug.Log("\t Unit Evaulate Score: [" + score + "]");

                    score = AIRule.EvaluateDuplicateInHand.Evaulate(op);
                    option.score += score;
                    //Debug.Log("\t Duplicate Card in Hand Evaulate Score: [" + score + "]");

                    //Debug.Log("Total: " + option.score);

                    list.Add(option);
                }

                return list;
            }
        }
    }
}
