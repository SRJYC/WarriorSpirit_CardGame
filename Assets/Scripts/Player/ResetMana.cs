using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMana : MonoBehaviour
{
    public GameEvent m_TurnStartEvent;

    public List<ManaObject> m_ManaList;
    
    public void Init(ManaObject mana1, ManaObject mana2)
    {
        m_ManaList = new List<ManaObject>();
        m_ManaList.Add(mana1);
        m_ManaList.Add(mana2);
    }

    private void Start()
    {
        m_TurnStartEvent.RegisterListenner(TurnStart);
    }

    private void OnDestroy()
    {
        m_TurnStartEvent.UnregisterListenner(TurnStart);
    }

    void TurnStart(GameEventData data)
    {
        foreach (ManaObject mana in m_ManaList)
        {
            mana.Increase();
            mana.Reset();
        }
    }

}
