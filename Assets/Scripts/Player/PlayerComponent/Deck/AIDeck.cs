using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeck : Deck
{
    public override void RegularDrawCard(int OptionNum = 3, int choiceNum = 1, int times = 1)
    {
        if (m_Hand.ReachMax)
        {
            Notify();
        }
        for (int i = 0; i < times; i++)
        {
            //get cards
            List<UnitData> cards = m_Deck.GetRandomCards(OptionNum);

            List<UnitData> result = AIManager.Instance.GetCardChoice(cards, choiceNum);

            AddToHand(result);
        }
    }
}
