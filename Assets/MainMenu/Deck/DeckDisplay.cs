using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class DeckDisplay : MonoBehaviour
    {
        public CardCollection deck;

        public CardDisplay warriorDisplay;
        public List<CardDisplay> cardDisplays;

        public GameObject Warning;
        private void OnEnable()
        {
            Display();
        }
        private void Display()
        {
            bool valid = true;
            if (deck.m_Warrior == null)
            {
                warriorDisplay.gameObject.SetActive(false);
                valid = false;
            }
            else
            {
                warriorDisplay.gameObject.SetActive(true);
                warriorDisplay.Display(deck.m_Warrior);
            }

            for (int i = 0; i < cardDisplays.Count; i++)
            {
                if (deck.m_CardList[i] == null)
                {
                    cardDisplays[i].gameObject.SetActive(false);
                    valid = false;
                }
                else
                {
                    cardDisplays[i].gameObject.SetActive(true);
                    cardDisplays[i].Display(deck.m_CardList[i]);
                }
            }

            Warning.SetActive(!valid);
        }
    }
}
