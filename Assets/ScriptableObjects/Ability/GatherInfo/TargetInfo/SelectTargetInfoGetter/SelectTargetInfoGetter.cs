using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/AbilityInfoGetter/SelectTargetGetter")]
public class SelectTargetInfoGetter : TargetInfoGetter
{
    public List<FixedTargetInfoGetter> m_AvaliableTargetGetter;
    [HideInInspector]public List<FieldBlock> m_AvaliableBlocks;

    public GameEvent m_BlockClickEvent;
    public GameEvent m_CancalSelectEvent;

    public int m_Num;

    [Tooltip("Can select same target multiple times")]
    public bool m_CanRepeat;

    [Tooltip("Will show confirm button and can continue without getting max number of targets")]
    public bool m_CanEndEarly;

    [Tooltip("Highlight block as well which means player can select empty block.(If need unit, units will be highlighted)")]
    public bool m_HighlightBlock;

    public HighlightType m_avaliableCardType;
    public HighlightType m_avaliableBlockType;
    public HighlightType m_selectedCardType;
    public HighlightType m_selectedBlockType;

    private int numberLeft;
    public override void GetInfo(Unit source)
    {
        m_Done = false;

        Reset();

        numberLeft = m_Num;
        DisplayInfo();
        HighlightAvaliableTarget(source);
        Register();

    }

    private void DisplayInfo()
    {
        ActionManager.Instance.m_ActionMessage.Display("Please select "+numberLeft+" more targets");

        if (m_CanEndEarly)
        {
            ActionManager.Instance.m_ConfirmButton.Show();
            ActionManager.Instance.m_ConfirmButton.callback += Done;
        }
    }

    private void HighlightAvaliableTarget(Unit source)
    {
        m_AvaliableBlocks = new List<FieldBlock>();
        foreach (FixedTargetInfoGetter infoGetter in  m_AvaliableTargetGetter)
        {
            infoGetter.GetInfo(source);
            m_AvaliableBlocks.AddRange(infoGetter.m_Blocks);
        }

        foreach (FieldBlock block in m_AvaliableBlocks)
        {
            HighlightTarget(block, false);
        }
    }

    public void SelectTrigger(GameEventData eventData)
    {
        SingleBlockData data = eventData.CastDataType<SingleBlockData>();
        if (data == null)
            return;

        if(m_AvaliableBlocks.Contains(data.m_Block))
        {
            if (!m_HighlightBlock && data.m_Block.m_Unit == null)
                return;

            Select(data.m_Block);
        }
    }

    private void Select(FieldBlock block)
    {
        if (!m_CanRepeat && m_Blocks.Contains(block))
        {
            UnselectTarget(block);
            return;
        }

        if (numberLeft <= 0)
            return;

        m_Blocks.Add(block);

        HighlightTarget(block, true);

        numberLeft--;
        //Debug.Log("Get one, Left : " + numberLeft);

        DisplayInfo();

        if (numberLeft == 0)
            Done();
    }

    private void Register()
    {
        m_BlockClickEvent.RegisterListenner(SelectTrigger);
        m_CancalSelectEvent.RegisterListenner(Cancel);
    }

    private void Unregister()
    {
        m_BlockClickEvent.UnregisterListenner(SelectTrigger);
        m_CancalSelectEvent.UnregisterListenner(Cancel);
    }

    public void Cancel(GameEventData data = null)
    {
        //Debug.Log("Cancel Select : " + m_Blocks.Count);
        if (m_Blocks.Count == 0)
        {
            HideInfo();
            Unregister();
            UnhighlightAll();

            ActionManager.Instance.CancelAction();
        }
        else
            UnselectTarget();
    }

    private void UnselectTarget(FieldBlock block = null)
    {
        if(block == null)
        {
            //get last target
            FieldBlock lastblock = m_Blocks[m_Blocks.Count - 1];
            m_Blocks.RemoveAt(m_Blocks.Count - 1);

            if (!m_Blocks.Contains(lastblock))
                HighlightTarget(lastblock, true, false);
        }
        else
        {
            m_Blocks.Remove(block);
            if (!m_Blocks.Contains(block))
                HighlightTarget(block, true, false);
        }

        numberLeft++;
        DisplayInfo();
    }

    private void Done()
    {
        HideInfo();

        UnhighlightAll();

        Unregister();

        Filter();

        m_Done = true;
    }

    private void HideInfo()
    {
        ActionManager.Instance.m_ActionMessage.Hide();

        if (m_CanEndEarly)
        {
            ActionManager.Instance.m_ConfirmButton.Hide();
        }
    }

    private void UnhighlightAll()
    {
        foreach (FieldBlock block in m_AvaliableBlocks)
        {
            HighlightTarget(block, false, false);
            HighlightTarget(block, true, false);
        }
    }

    private void HighlightTarget(FieldBlock block, bool isSelect, bool highlight = true)
    {
        HighlightType cardType = isSelect ? m_selectedCardType : m_avaliableCardType;
        HighlightType blockType = isSelect ? m_selectedBlockType : m_avaliableBlockType;

        if (m_HighlightBlock)
        {
            if (highlight)
                HighlightManager.Instance.Highlight(block.gameObject, blockType);
            else
                HighlightManager.Instance.Unhighlight(block.gameObject, blockType);
        }

        if (m_NeedUnit && block.m_Unit != null)
        {
            GameObject go = block.m_Unit.gameObject;

            if (highlight)
                HighlightManager.Instance.Highlight(go, cardType);
            else
                HighlightManager.Instance.Unhighlight(go, cardType);
        }
    }
}
