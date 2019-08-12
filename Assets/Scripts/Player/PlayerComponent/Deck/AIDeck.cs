using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeck : Deck
{
    public override void Start()
    {

    }

    public override void DrawCard(Notify n, SingleUnitCondition condition = null, int OptionNum = 3, int choiceNum = 1, int times = 1)
    {
        notify = n;
        if (m_Hand.ReachMax)
        {
            NotifyCallback();
            return;
        }

        for (int i = 0; i < times; i++)
        {
            //get cards
            List<UnitData> cards = new List<UnitData>();
            if (condition == null)
                cards = m_Deck.GetRandomCards(OptionNum);
            else
                cards = m_Deck.GetRandomCardsWithCondition(condition, OptionNum);

            List<UnitData> result = new List<UnitData>();
            result.Add(AIPlayer.AIManager.Instance.DrawCard(cards));

            AddToHand(result);
        }
    }
}
