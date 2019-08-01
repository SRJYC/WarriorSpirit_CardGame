using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPlayer
{
    public class AIPlayCardTrigger : MonoBehaviour
    {
        public GameEvent startPlayEvent;
        public GameEvent stopPlayEvent;

        // Start is called before the first frame update
        void Start()
        {
            startPlayEvent.RegisterListenner(StartPlay);
            stopPlayEvent.RegisterListenner(StopPlay);
        }

        private void OnDestroy()
        {
            startPlayEvent.UnregisterListenner(StartPlay);
            stopPlayEvent.UnregisterListenner(StopPlay);
        }

        public void StartPlay(GameEventData data)
        {
            AIManager.Instance.Play();
        }

        public void StopPlay(GameEventData data)
        {
            AIManager.Instance.Stop();
        }
    }
}

