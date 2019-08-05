using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    protected PlayerID m_ID;
    protected Hand m_Hand;
    protected CardCollection m_Deck;
    public int CardCount
    {
        get
        {
            if (m_Deck != null)
                return m_Deck.m_CardList.Count;
            else
                return 0;
        }
    }

    [Header("Inital")]
    public int m_initalCardsNumber;

    [HideInInspector]public bool notifyDeckManager;

    [Header("Draw Card")]
    private DeckDrawCard m_DrawCardComponent;
    public SelectFromCardOptions m_PlayerSelect;
    public GameEvent m_DisplayCardOptionEvent;
    public TextProperty message;
    private void Start()
    {
        m_DrawCardComponent = new DeckDrawCard(this,m_PlayerSelect, m_DisplayCardOptionEvent,message.ToString());
    }

    public void Init(Hand hand, CardCollection deck, PlayerID id)
    {
        m_Hand = hand;
        m_Deck = deck;
        m_ID = id;

        //get initial cards
        AddToHand(m_Deck.GetRandomCards(m_initalCardsNumber));
    }

    /// <summary>
    /// Display random cards from deck. Player choose cards from it and add them to hand.
    /// </summary>
    /// <param name="OptionNum">The number of random cards from deck</param>
    /// <param name="choiceNum">The number of cards to choose in one selection</param>
    /// <param name="times">The times of selection</param>
    public virtual void RegularDrawCard(int OptionNum = 3, int choiceNum = 1, int times = 1)
    {
        if (m_Hand.ReachMax)
        {
            Notify();
        }
        else
        {
            StartCoroutine(DrawCard(OptionNum, choiceNum, times));
        }
    }

    private IEnumerator DrawCard(int OptionNum, int choiceNum, int times)
    {
        //Debug.Log("Get Triggered");
        for(int i = 0; i < times; i++)
        {
            //Debug.Log("Get Cards");
            //get cards
            List<UnitData> cards = m_Deck.GetRandomCards(OptionNum);

            //Debug.Log("Display options");

            m_DrawCardComponent.DisplayOptions(cards);

            //Debug.Log("Choice");

            yield return StartCoroutine(m_DrawCardComponent.GetPlayerChoice(choiceNum));
        }
    }

    public void AddToHand(List<UnitData> cards)
    {
        foreach(UnitData data in cards)
        {
            GameObject card = DeckManager.Instance.GetCard(data, m_ID);

            m_Hand.AddCard(card);
        }

        if (notifyDeckManager)
        {
            Notify();
        }
    }

    protected void Notify()
    {
        DeckManager.Instance.EndDrawCard(m_ID);
        notifyDeckManager = false;
    }

    public void AddToDeck(UnitData data)
    {
        if(!m_Deck.m_CardList.Contains(data))
        {
            Debug.Log("[" + data.name + "] add to deck");
            m_Deck.m_CardList.Add(data);
        }
    }
}
