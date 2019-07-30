using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckDrawCard
{
    public readonly Deck m_Owner;
    public readonly SelectFromCardOptions m_PlayerSelect;
    public readonly GameEvent m_DisplayCardOptionEvent;

    [Tooltip("Message to show when display options.")]
    public readonly string m_Message;

    private IEnumerator coroutine;

    public DeckDrawCard(Deck deck, SelectFromCardOptions selectMethod, GameEvent cardOptionsEvent, string msg)
    {
        m_Owner = deck;
        m_PlayerSelect = selectMethod;
        m_DisplayCardOptionEvent = cardOptionsEvent;
        m_Message = msg;
    }

    public void DisplayOptions(List<UnitData> cards)
    {
        CardSelectOptionsData data = new CardSelectOptionsData();

        data.m_Switch = true;
        data.isCondition = false;
        data.unitDatas = cards.ToArray();

        m_DisplayCardOptionEvent.Trigger(data);
    }

    public IEnumerator GetPlayerChoice(int choiceNum)
    {
        m_PlayerSelect.m_Num = choiceNum;
        m_PlayerSelect.m_Message = m_Message;
        m_PlayerSelect.GetInfo(Cancel);

        while (!m_PlayerSelect.m_Done)
        {
            yield return new WaitForSeconds(.1f);
        }

        if (m_PlayerSelect.m_selectedCardPreviewList.Count > 0)
        {
            //Debug.Log("Get Data");
            List<UnitData> datas = new List<UnitData>();
            foreach(GameObject cardPreview in m_PlayerSelect.m_selectedCardPreviewList)
            {
                UnitData data = cardPreview.GetComponent<CardDisplay>().m_CardData;
                datas.Add(data);
            }

            m_Owner.AddToHand(datas);

            HideWindow();
        }

    }

    /// <summary>
    /// Card draw can't be cancelled
    /// </summary>
    public void Cancel()
    {
        return;
    }

    public void HideWindow()
    {
        CardSelectOptionsData data2 = new CardSelectOptionsData();
        data2.m_Switch = false;
        m_DisplayCardOptionEvent.Trigger(data2);
    }
}
