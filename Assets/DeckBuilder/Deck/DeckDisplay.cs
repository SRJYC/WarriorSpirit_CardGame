using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeckBuilder
{ 
    public class DeckDisplay : MonoBehaviour
    {
        public CardCollection deck;

        public SingleCardDisplay warriorDisplay;
        public List<SingleCardDisplay> cardDisplays;

        public bool hasChange = false;

        public const int CopyLimit = 3;

        public void Start()
        {
            for (int i = 0; i < cardDisplays.Count; i++)
            {
                cardDisplays[i].Init(this, i);
            }
        }

        public void ChangeDeck(CardCollection cc)
        {
            //if (hasChange)
            //    return;

            Display(cc);
        }

        private void Display(CardCollection cc)
        {
            deck = cc;

            warriorDisplay.Display(deck.m_Warrior);

            for (int i = 0; i < cardDisplays.Count; i++)
            {
                cardDisplays[i].Display(deck.m_CardList[i]);
            }
        }

        public void ChangeCard(int index, UnitData data)
        {
            if(index < 0 && data.IsWarrior)
            {
                deck.m_Warrior = data;
                warriorDisplay.Display(data);

                hasChange = true;
            }
            else if(NumOfCard(data) < CopyLimit)
            {
                deck.m_CardList[index] = data;
                cardDisplays[index].Display(data);

                hasChange = true;
            }
        }

        private int NumOfCard(UnitData data)
        {
            int n = 0;
            foreach(UnitData card in deck.m_CardList)
            {
                if (card == deck)
                    n++;
            }

            return n;
        }


        [ContextMenu("Test")]
        public void Test()
        {
            Display(deck);
        }
    }
}
