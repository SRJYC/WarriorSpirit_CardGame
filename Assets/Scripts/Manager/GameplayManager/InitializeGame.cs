using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    public GameEvent m_GameStartEvent;
    public GameEvent m_EndInitializeEvent;

    // Start is called before the first frame update
    void Start()
    {
        m_GameStartEvent.RegisterListenner(Init);
    }

    private void OnDestroy()
    {
        m_GameStartEvent.UnregisterListenner(Init);
    }

    void Init(GameEventData data)
    {
        //set player Id
        BoardManager.Instance.SetID(GameData.Instance.playerId != PlayerID.Player1);

        PlayerManager.Instance.Init();

        Invoke("EndInitialize", 1);
    }

    void EndInitialize()
    {
        m_EndInitializeEvent.Trigger();
    }
}
