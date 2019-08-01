using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AIPlayer
{
    namespace AIMove
    {
        public class Common
        {
            public const float TOLERANCE = 10;

            public static T RandomOptionOfBest<T>(List<T> options) where T : Option
            {
                options = options.OrderByDescending(op => op.score).ToList();
                int highest = options[0].score;

                List<T> possible = new List<T>();
                foreach (T op in options)
                {
                    if (op.score >= highest - Common.TOLERANCE)
                        possible.Add(op);
                }
                int index = Random.Range(0, possible.Count);

                return possible[index];
            }
        }
    }
}
