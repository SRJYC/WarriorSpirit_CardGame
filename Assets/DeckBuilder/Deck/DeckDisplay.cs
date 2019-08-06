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

        public const int CopyLimit = 3;

        public void Start()
        {
            warriorDisplay.Init(this, -1);
            for (int i = 0; i < cardDisplays.Count; i++)
            {
                cardDisplays[i].Init(this, i);
            }
        }

        public void ChangeDeck(CardCollection cc)
        {
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
            //Debug.Log("index:" + index);
            if(index < 0 && data.IsWarrior)
            {
                deck.m_Warrior = data;
                warriorDisplay.Display(data);
            }
            else if(index >=0 && NumOfCard(data) < CopyLimit)
            {
                //Debug.Log(NumOfCard(data));
                deck.m_CardList[index] = data;
                cardDisplays[index].Display(data);
            }
        }

        private int NumOfCard(UnitData data)
        {
            int n = 0;
            foreach(UnitData card in deck.m_CardList)
            {
                if (card == data)
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
