using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityInfoGetter/SelectFromCardOptions")]
public class SelectFromCardOptions : SelectTargetInfoGetter
{
    public delegate void CancelCallback();
    public CancelCallback cancelCallback;

    [Header("Can cancel selection")]
    public bool m_CanCancel = true;

    [Header("Words to Display")]
    public string m_Message;

    [HideInInspector]public List<GameObject> m_selectedCardPreviewList;

    public void GetInfo(CancelCallback method)
    {
        cancelCallback = method;

        Unit none = null;
        GetInfo(none);
    }

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

            m_selectedCardPreviewList = new List<GameObject>();

            numberLeft = m_Num;

            DisplayInfo();

            Unregister();
            Register();
        }
    }

    public override void StoreInfo(AbilityInfo info)
    {
        throw new System.NotImplementedException();
    }

    private void DisplayInfo()
    {
        ActionManager.Instance.m_ActionMessage.Display(m_Message + "(" + numberLeft + "/" + m_Num +")");

        ActionManager.Instance.m_ConfirmButton.Show();
        ActionManager.Instance.m_ConfirmButton.callback += Done;
    }

    public override void SelectTrigger(GameEventData eventData)
    {
        //Debug.Log("Select");
        CardPreviewData data = eventData.CastDataType<CardPreviewData>();
        if (data == null)
            return;

        //Debug.Log("Check Select");
        GameObject cardPreview = data.m_Card;
        if (!m_CanRepeat && m_selectedCardPreviewList.Contains(cardPreview))
        {
            //Debug.Log("Repeat Select");
            UnselectTarget(cardPreview);
            return;
        }

        if (numberLeft <= 0)
            return;

        //Debug.Log("Confirm Select");
        m_selectedCardPreviewList.Add(cardPreview);

        HighlightTarget(cardPreview);

        numberLeft--;

        DisplayInfo();
    }

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

    public override void Cancel(GameEventData data = null)
    {
        //Debug.Log("Cancel Select : " + m_Blocks.Count);
        if (m_selectedCardPreviewList.Count == 0)
        {
            if (!m_CanCancel)
                return;

            HideInfo();
            Unregister();
            UnhighlightAll();

            if (cancelCallback != null)
                cancelCallback.Invoke();
            else
                ActionManager.Instance.CancelAction();

            cancelCallback = null;
        }
        else
            UnselectTarget();
    }

    private void UnselectTarget(GameObject cardPreview = null)
    {
        if (cardPreview == null)
        {
            //get last target
            GameObject lastCard = m_selectedCardPreviewList[m_selectedCardPreviewList.Count - 1];
            m_selectedCardPreviewList.RemoveAt(m_selectedCardPreviewList.Count - 1);

            if (!m_selectedCardPreviewList.Contains(lastCard))
                HighlightTarget(lastCard, false);
        }
        else
        {
            m_selectedCardPreviewList.Remove(cardPreview);
            if (!m_selectedCardPreviewList.Contains(cardPreview))
                HighlightTarget(cardPreview, false);
        }

        numberLeft++;
        DisplayInfo();
    }

    private void Done()
    {
        if (!m_CanEndEarly && numberLeft > 0)
            return;

        cancelCallback = null;

        HideInfo();

        UnhighlightAll();

        Unregister();

        m_Done = true;
    }

    private void HideInfo()
    {
        ActionManager.Instance.m_ActionMessage.Hide();

        ActionManager.Instance.m_ConfirmButton.Hide();
    }

    private void UnhighlightAll()
    {
        foreach(GameObject card in m_selectedCardPreviewList)
        {
            HighlightTarget(card, false);
        }
    }

    private void HighlightTarget(GameObject card, bool highlight = true)
    {
        if (highlight)
            HighlightManager.Instance.Highlight(card, m_HighlightOptions.m_selectedCardType);
        else
            HighlightManager.Instance.Unhighlight(card, m_HighlightOptions.m_selectedCardType);
    }
}
