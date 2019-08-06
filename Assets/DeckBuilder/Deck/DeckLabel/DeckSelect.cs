using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DeckBuilder
{
    public class DeckSelect : MonoBehaviour
        ,IPointerClickHandler
    {
        [Header("Text")]
        public TMPro.TextMeshProUGUI m_Text;

        [Header("Background")]
        public Image background;
        public Color normalColor;
        public Color selectColor;

        [Header("Deck")]
        private DeckBuilder m_Owner;
        public CardCollection m_Deck;
        [SerializeField]private bool m_Select;
        public void Init(DeckBuilder builder, CardCollection collection)
        {
            m_Owner = builder;
            m_Deck = collection;

            Display();
        }

        public void ChangeSelect(bool select)
        {
            if (m_Select == select)
                return;

            m_Select = select;
            Display();
        }

        public void Display()
        {
            m_Text.text = m_Deck.name;
            background.color = m_Select ? selectColor : normalColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            m_Owner.Select(this);
        }

    }
}
