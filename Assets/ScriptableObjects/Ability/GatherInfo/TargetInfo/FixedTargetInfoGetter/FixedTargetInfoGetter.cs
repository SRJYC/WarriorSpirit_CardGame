using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FixedTargetInfoGetter : TargetInfoGetter
{
    [Header("Enemy or Ally Field?")]
    public bool m_IsAlly;

    [Header("Condition For Unit (Need Unit and Not Need Empty)")]
    [Tooltip("True means Unit which satisfy condition will be excluded, false means Unit which doesn't satisfy condition will be excluded.")]
    public bool m_Exclude;
    public SingleUnitCondition m_Condition = null;

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

    protected override void Filter()
    {
        base.Filter();

        if(m_NeedUnit && !m_NeedEmpty && m_Condition != null)
        {
            for(int i = m_Blocks.Count - 1; i>=0; i--)
            {
                bool check = m_Condition.Check(m_Blocks[i].m_Unit.m_Data);
                if(check && m_Exclude)
                {
                    m_Blocks.RemoveAt(i);
                }
                else if(!check && !m_Exclude)
                {
                    m_Blocks.RemoveAt(i);
                }
            }

            m_Targets = new List<Unit>();
            GetUnitsFromBlocks();
        }
    }

    protected abstract void ConcreteGather();
}
