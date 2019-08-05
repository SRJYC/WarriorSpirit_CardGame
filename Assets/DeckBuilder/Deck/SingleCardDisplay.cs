using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DeckBuilder
{
    public class SingleCardDisplay : MonoBehaviour
        ,IDropHandler
    {
        public CardDisplay cardDisplay;

        private int index;
        private DeckDisplay deck;

        public void Init(DeckDisplay dd, int i)
        {
            deck = dd;
            index = i;
        }

        public void OnDrop(PointerEventData eventData)
        {
            UnitData data = eventData.pointerDrag.GetComponent<CardDisplay>().m_CardData;

            deck.ChangeCard(index, data);
        }

        public void Display(UnitData data)
        {
            if (data == null)
                cardDisplay.gameObject.SetActive(false);
            else
            {
                cardDisplay.Display(data);
                cardDisplay.gameObject.SetActive(true);
            }
        }
    }
}

