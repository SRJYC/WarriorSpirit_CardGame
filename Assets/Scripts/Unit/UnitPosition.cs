using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPosition
{
    public readonly Unit m_Owner;
    public FieldBlock m_Block;

    public UnitPosition(Unit unit)
    {
        m_Owner = unit;
    }

    public void MoveTo(FieldBlock block)
    {
        //remove from last block
        if (m_Block != null)
            m_Block.SetUnit(null);

        //Debug.Log(m_Owner + " move to " + block);
        //add to new block
        m_Block = block;
        m_Block.SetUnit(m_Owner);
        //Debug.Log(m_Owner + " is on " + m_Block);

        //move game object
        m_Owner.transform.SetParent(m_Block.transform);
    }

    public void RemoveFromBoard()
    {
        if (m_Block != null)
            m_Block.SetUnit(null);
        m_Block = null;
    }
}
