using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBlock : MonoBehaviour
{
    [Header("Front, Center, Back")]
    public FieldBlockType m_RowType;

    [Header("Left, Middle, Right")]
    public FieldBlockType m_ColumnType;


    public PlayerID m_PlayerID;

    public Unit m_Unit { get; private set; }

    public void SetUnit(Unit unit)
    {
        m_Unit = unit;

        if (m_Unit == null)
            return;

        m_Unit.transform.position = this.transform.position;
    }
}
