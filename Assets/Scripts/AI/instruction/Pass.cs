using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPlayer
{
    namespace AIAction
    {
        public class Pass
        {
            public static void Do()
            {
                Debug.Log("AI Pass turn");

                AIManager.Instance.Pass();
            }
        }
    }
}
