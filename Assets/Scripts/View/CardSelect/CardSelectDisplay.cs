using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectDisplay : MonoBehaviour
{
    public GameObject m_PreviewCardPrefab;
    public GameObject m_Container;

    private ObjectPool m_Pool;

    // Start is called before the first frame update
    void Start()
    {
        m_Pool = new ObjectPool(m_PreviewCardPrefab);

        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(Screen.width,rect.rect.height);

        gameObject.SetActive(false);
    }

    public void DisplayOptions(UnitData[] datas)
    {
        //Debug.Log("Reach");
        gameObject.SetActive(true);

        foreach (UnitData data in datas)
        {
            GameObject card = m_Pool.Get(true);

            card.GetComponent<CardDisplay>().Display(data);
            card.transform.SetParent(m_Container.transform);
            //card.transform.localScale = Vector3.one;

            card.GetComponent<HoverOnCardPreview>().enabled = false;
        }
    }

    public void DisplayOptions(RankUpCondition[] conditions)
    {
        gameObject.SetActive(true);

        foreach (RankUpCondition rankUp in conditions)
        {
            GameObject card = m_Pool.Get(true);

            card.GetComponent<CardDisplay>().Display(rankUp.m_HighRankSpirit);
            card.transform.SetParent(m_Container.transform);

            card.GetComponent<HoverOnCardPreview>().condition = rankUp;
            card.GetComponent<HoverOnCardPreview>().enabled = true;
        }
    }

    public void Hide()
    {
        m_Pool.DeactivateAll(true);
        gameObject.SetActive(false);
    }
}
