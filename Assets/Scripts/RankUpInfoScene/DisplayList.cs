using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayList : MonoBehaviour
{
    public GameObject m_PreviewCardPrefab;
    public GameObject m_Container;

    private ObjectPool m_Pool;

    // Start is called before the first frame update
    void Start()
    {
        m_Pool = new ObjectPool(m_PreviewCardPrefab);
    }

    public void Display(UnitData[] datas)
    {
        m_Pool.DeactivateAll(true);

        foreach (UnitData data in datas)
        {
            GameObject card = m_Pool.Get(true);

            card.GetComponent<CardDisplay>().Display(data);
            card.transform.SetParent(m_Container.transform);
        }
    }
}
