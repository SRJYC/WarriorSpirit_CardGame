﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/AbilityInfoGetter/SelectTargetGetter")]
public class SelectTargetInfoGetter : TargetInfoGetter
{
    [Header("Select Range")]
    public List<FixedTargetInfoGetter> m_AvaliableTargetGetter;
    [HideInInspector]public List<FieldBlock> m_AvaliableBlocks;

    [Header("Select Event")]
    public GameEvent m_SelectEvent;
    public GameEvent m_CancalSelectEvent;

    [Header("Quantity")]
    public int m_Num;

    [Header("Select Options")]
    [Tooltip("Can select same target multiple times")]
    public bool m_CanRepeat;
    [Tooltip("Will show confirm button and can continue without getting max number of targets")]
    public bool m_CanEndEarly;

    [Header("Highlight Options")]
    [Tooltip("Highlight block as well which means player can select empty block.(If need unit, units will be highlighted)")]
    public bool m_HighlightBlock;
    public HighlightOptionsForSelection m_HighlightOptions;

    protected int numberLeft;

    [HideInInspector] public bool EnemySelect = false;
    public override void GetInfo(Unit source)
    {
        if (EnemySelect)
        {
            Filter();
            m_Done = true;
            EnemySelect = false;
        }
        else
        {
            m_Done = false;

            Reset();
            numberLeft = m_Num;
            DisplayInfo();
            HighlightAvaliableTarget(source);

            Unregister();
            Register();
        }
    }

    public void GetAvaliableTargets(Unit source)
    {
        m_AvaliableBlocks = new List<FieldBlock>();
        foreach (FixedTargetInfoGetter infoGetter in m_AvaliableTargetGetter)
        {
            infoGetter.GetInfo(source);
            m_AvaliableBlocks.AddRange(infoGetter.m_Blocks);
        }
    }

    private void Done()
    {
        HideInfo();

        UnhighlightAll();

        Unregister();

        Filter();

        m_Done = true;
    }

    #region ability message
    private void DisplayInfo()
    {
        int select = m_Num - numberLeft;
        ActionManager.Instance.m_ActionMessage.Display(select +"/"+ m_Num);

        if (m_CanEndEarly)
        {
            ActionManager.Instance.m_ConfirmButton.Show();
            ActionManager.Instance.m_ConfirmButton.callback += Done;
        }
    }

    private void HideInfo()
    {
        ActionManager.Instance.m_ActionMessage.Hide();

        if (m_CanEndEarly)
        {
            ActionManager.Instance.m_ConfirmButton.Hide();
        }
    }
    #endregion

    #region Event for select and cancel
    private void Register()
    {
        m_SelectEvent.RegisterListenner(SelectTrigger);
        m_CancalSelectEvent.RegisterListenner(Cancel);
    }

    private void Unregister()
    {
        m_SelectEvent.UnregisterListenner(SelectTrigger);
        m_CancalSelectEvent.UnregisterListenner(Cancel);
    }
    #endregion


    #region Select and Cancel
    public virtual void SelectTrigger(GameEventData eventData)
    {
        SingleBlockData data = eventData.CastDataType<SingleBlockData>();
        if (data == null)
            return;

        if(m_AvaliableBlocks.Contains(data.m_Block))
        {
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


    public virtual void Cancel(GameEventData data = null)
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

    #endregion

    #region highlight
    private void HighlightAvaliableTarget(Unit source)
    {
        GetAvaliableTargets(source);

        foreach (FieldBlock block in m_AvaliableBlocks)
        {
            HighlightTarget(block, false);
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
        HighlightType cardType = isSelect ? m_HighlightOptions.m_selectedCardType : m_HighlightOptions.m_avaliableCardType;
        HighlightType blockType = isSelect ? m_HighlightOptions.m_selectedBlockType : m_HighlightOptions.m_avaliableBlockType;

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
    #endregion
}
