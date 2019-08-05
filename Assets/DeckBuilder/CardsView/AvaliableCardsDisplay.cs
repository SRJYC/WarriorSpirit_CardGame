using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeckBuilder
{
    public class AvaliableCardsDisplay : MonoBehaviour
    {
        public GameObject m_Prefab;
        public CardCollection m_Spirits;
        public CardCollection m_Warriors;

        public GameObject m_Container;

        public bool isDisplaySpirit;
        public ObjectPool m_Pool;

        private void Start()
        {
            m_Pool = new ObjectPool(m_Prefab);
            Display();
        }

        private void OnDestroy()
        {
            m_Pool.Clear();
        }

        public void Display()
        {
            m_Pool.DeactivateAll(true);

            CardCollection cards = isDisplaySpirit ? m_Warriors : m_Spirits;

            isDisplaySpirit = !isDisplaySpirit;

            foreach(UnitData data in cards.m_CardList)
            {
                GameObject go = m_Pool.Get(true);

                go.GetComponent<CardDisplay>().Display(data);

                go.transform.SetParent(m_Container.transform);
            }
        }
    }
}


