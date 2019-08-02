using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPlayer
{
    public class AIUseActionTrigger : MonoBehaviour
    {
        public GameEvent startPlayEvent;

        // Start is called before the first frame update
        void Start()
        {
            startPlayEvent.RegisterListenner(StartPlay);
        }

        private void OnDestroy()
        {
            startPlayEvent.UnregisterListenner(StartPlay);
        }

        public void StartPlay(GameEventData eventData)
        {
            //Debug.Log("Start Play");
            StateChangeData data = eventData.CastDataType<StateChangeData>();
            if (data == null)
                return;

            //Debug.Log("Start Play2");
            if (data.currentStates.Contains(StateID.EnemyTurn))
                AIManager.Instance.UseAction();
            //Debug.Log("Start Play3");
        }
    }
}
