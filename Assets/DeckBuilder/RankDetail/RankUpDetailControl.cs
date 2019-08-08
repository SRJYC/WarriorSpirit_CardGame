using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DeckBuilder
{
    public class RankUpDetailControl : MonoBehaviour
        ,IDropHandler
    {
        public RankInfoSceneManager rankInfo;
        // Start is called before the first frame update
        void Start()
        {
            rankInfo.gameObject.SetActive(false);
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("Drop");
            UnitData data = eventData.pointerDrag.GetComponent<CardDisplay>().m_CardData;
            Display(data);
        }

        public void Display(UnitData data)
        {
            rankInfo.m_SelectedSpirit = data;
            rankInfo.firstDisplay = true;

            rankInfo.gameObject.SetActive(true);
        }

        public void Hide()
        {
            rankInfo.gameObject.SetActive(false);
        }
    }
}
