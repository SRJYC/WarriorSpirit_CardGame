using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject prefab;
    private List<GameObject> m_Actives = new List<GameObject>();
    private List<GameObject> m_Inactives = new List<GameObject>();
    
    public ObjectPool(GameObject gameObject)
    {
        prefab = gameObject;
    }

    public GameObject Get(bool enable = false)
    {

        GameObject gameObject = null;
        if (m_Inactives.Count > 0)
        {
            gameObject = GetInactive();
        }
        else
        {
            gameObject = MonoBehaviour.Instantiate(prefab);
        }


        m_Actives.Add(gameObject);

        if (enable)
            gameObject.SetActive(true);

        return gameObject;
    }

    public void Deactivate(GameObject gameObject, bool disable = false)
    {
        m_Actives.Remove(gameObject);
        m_Inactives.Add(gameObject);

        if (disable)
            gameObject.SetActive(false);
    }

    public void DeactivateAll(bool disable = false)
    {
        for(int i=m_Actives.Count-1; i>=0;i--)
        {
            m_Inactives.Add(m_Actives[i]);

            if (disable)
                m_Actives[i].SetActive(false);

            m_Actives.RemoveAt(i);
        }
    }

    private GameObject GetInactive()
    {
        GameObject gameObject = m_Inactives[m_Inactives.Count - 1];
        m_Inactives.RemoveAt(m_Inactives.Count - 1);
        return gameObject;
    }

    public void Clear(bool destroy = true)
    {
        ClearList(m_Actives, destroy);
        ClearList(m_Inactives, destroy);
    }

    private void ClearList(List<GameObject> list, bool destroy)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (destroy)
                GameObject.Destroy(list[i]);

            list.RemoveAt(i);
        }
    }
}
