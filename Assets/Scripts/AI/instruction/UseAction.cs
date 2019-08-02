using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPlayer
{
    namespace AIAction
    {
        public class UseAction
        {
            public static void Do(AIMove.ActionOption option)
            {
                ActionManager.Instance.TriggerAction(option.ability, false, false);
            }
        }
    }
}
