using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPlayer
{
    namespace AIMove
    {
        public class Option
        {
            public int score = 0;
        }

        public class UnitOption : Option
        {
            public readonly UnitData data;

            public UnitOption(UnitData unit)
            {
                data = unit;
            }
        }

        public class RankUpOption : Option
        {
            public readonly UnitData data;

            public readonly UnitData spirit1;
            public readonly UnitData spirit2;

            public readonly FieldBlock block;
            public RankUpOption(UnitData highRank, UnitData material1, UnitData material2, FieldBlock position)
            {
                data = highRank;
                spirit1 = material1;
                spirit2 = material2;
                block = position;
            }
        }

        public class ActionOption : Option
        {
            public readonly Ability ability;
            public ActionOption(Ability a)
            {
                ability = a;
            }
        }

        public class TargetOption : Option
        {
            public readonly FieldBlock block;
            public TargetOption(FieldBlock b)
            {
                block = b;
            }
        }
    }
}
