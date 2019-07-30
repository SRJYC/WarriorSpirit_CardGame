using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour
{
    public UnitData m_Data;
    public UnitPosition m_Position;
    public UnitStatus m_Status;
    public UnitDestroy m_Destroy;

    public PlayerID m_PlayerID;

    public UnitEvent m_DestroyEvent;
    public UnitEvent m_StatsChangeEvent;

    private bool init = false;
    public void Init(UnitData data, PlayerID id)
    {
        if (!data)
            return;

        m_Data = data.Clone(this);

        GetComponent<CardDisplay>().Display(m_Data);

        m_Position = new UnitPosition(this);
        m_Status = new UnitStatus(this);
        m_Destroy = new UnitDestroy(this, m_DestroyEvent, m_StatsChangeEvent);

        m_PlayerID = id;

        init = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!init)
            Init(m_Data, PlayerID.Player1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool PlayerControl()
    {
        if (PlayerManager.Instance.test)
            return true;

        return OrderManager.Instance.CurrentUnit == this && PlayerManager.Instance.GetLocalPlayerID() == m_PlayerID;
    }
}
