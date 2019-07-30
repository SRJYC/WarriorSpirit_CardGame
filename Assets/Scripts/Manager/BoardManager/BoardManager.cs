using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    public FieldController m_Player1Field;
    public FieldController m_Player2Field;

    public void SetID(bool inverse = false)
    {
        if (!inverse)
        {
            m_Player1Field.SetID(PlayerID.Player1);
            m_Player2Field.SetID(PlayerID.Player2);
        }
        else
        {
            m_Player1Field.SetID(PlayerID.Player2);
            m_Player2Field.SetID(PlayerID.Player1);
        }
        
    }

    public FieldController GetFieldController(PlayerID id, bool opposite = false)
    {
        if(opposite)
            return id == PlayerID.Player1 ? m_Player2Field : m_Player1Field;
        else
            return id == PlayerID.Player1 ? m_Player1Field : m_Player2Field;
    }
}
