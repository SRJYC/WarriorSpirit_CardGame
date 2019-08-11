using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetInfoGetter : AbilityInfoGetter
{
    [Header("Condition, Select Both If Need All Blocks")]
    public bool m_NeedUnit = true;
    public bool m_NeedEmpty = false;

    [Header("If need unit, then:")]
    public bool m_ExculdeSelf;
    public bool m_ExcludeWarrior;

    protected FieldBlock m_Position;
    protected FieldController m_Field;

    [HideInInspector] public List<Unit> m_Targets;
    [HideInInspector] public List<FieldBlock> m_Blocks;

    protected void Filter()
    { 
        ExculdeBlocks();

        if (m_NeedUnit)
            GetUnitsFromBlocks();
    }

    public override void StoreInfo(AbilityInfo info)
    {
        TargetInfo targetInfo = info.CastInfoType<TargetInfo>();
        if (targetInfo == null)
            return;

        targetInfo.m_Targets = m_Targets;
        targetInfo.m_Blocks = m_Blocks;
    }

    public void Reset()
    {
        m_Blocks = new List<FieldBlock>();
        m_Targets = new List<Unit>();
    }

    protected void GetUnitsFromBlocks()
    {
        foreach (FieldBlock block in m_Blocks)
        {
            if (block.m_Unit != null)
                m_Targets.Add(block.m_Unit);
        }
    }

    protected void ExculdeBlocks()
    {
        if (m_NeedUnit)
        {
            if(!m_NeedEmpty)
                m_Blocks.RemoveAll(block => block.m_Unit == null);

            if (m_ExculdeSelf)
                m_Blocks.RemoveAll(block => block == m_Position);

            if (m_ExcludeWarrior)
                m_Blocks.RemoveAll(block => block.m_RowType == FieldBlockType.Center);
        }
        else
        {
            if(m_NeedEmpty)
                m_Blocks.RemoveAll(block => block.m_Unit != null);
        }
    }
}
