using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FixedTargetInfoGetter : TargetInfoGetter
{
    [Header("Enemy or Ally Field?")]
    public bool m_IsAlly;

    public override void GetInfo(Unit source)
    {
        m_Done = false;

        Reset();

        //get position
        m_Position = source.m_Position.m_Block;

        //get field
        m_Field = m_IsAlly ? 
            BoardManager.Instance.GetFieldController(source.m_PlayerID) 
            : BoardManager.Instance.GetFieldController(source.m_PlayerID, true);

        ConcreteGather();

        Filter();

        m_Done = true;
    }

    protected abstract void ConcreteGather();
}
