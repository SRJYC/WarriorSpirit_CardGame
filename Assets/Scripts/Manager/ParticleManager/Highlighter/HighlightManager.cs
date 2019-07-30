using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightManager : Singleton<HighlightManager>
{
    [Tooltip("Reference for HighliterType")]
    public HighlightType type;

    [Header("Highlighter. (Order must be same as type)")]
    public List<GameObject> m_HighlighterList;

    public List<ObjectPool> m_Pools;
    public Dictionary<GameObject, Dictionary<HighlightType, GameObject>> reference;

    private void Start()
    {
        m_Pools = new List<ObjectPool>();
        foreach(GameObject gameObject in m_HighlighterList)
        {
            ObjectPool pool = new ObjectPool(gameObject);
            m_Pools.Add(pool);
        }

        reference = new Dictionary<GameObject, Dictionary<HighlightType, GameObject>>();
    }

    public void Highlight(GameObject gameObject, HighlightType type)
    {
        //Debug.Log("Try Highlight " + gameObject);
        bool newObject = true;
        Dictionary<HighlightType, GameObject> highlightList;
        if (reference.TryGetValue(gameObject, out highlightList))
        {
            //Debug.Log("Has " + gameObject);
            newObject = false;
            if (highlightList.ContainsKey(type))
                return;
            //Debug.Log("Doesnt highlight with" + type);
        }
        else
            highlightList = new Dictionary<HighlightType, GameObject>();

        GameObject highlighter = m_Pools[(int)type].Get();
        highlighter.GetComponent<Highlighter>().Highlight(gameObject);
        highlighter.transform.localScale = Vector3.one;

        highlightList.Add(type, highlighter);//add to list

        if(newObject)
            reference.Add(gameObject, highlightList);
    }

    public void Unhighlight(GameObject gameObject)
    {
        Dictionary<HighlightType, GameObject> highlightList;
        if (reference.TryGetValue(gameObject, out highlightList))
        {
            foreach(KeyValuePair<HighlightType, GameObject> pair in highlightList)
            {
                pair.Value.GetComponent<Highlighter>().Stop();
                m_Pools[(int)pair.Key].Deactivate(pair.Value);
            }
            highlightList.Clear();

            reference.Remove(gameObject);
        }
    }

    public void Unhighlight(GameObject gameObject, HighlightType type)
    {
        Dictionary<HighlightType, GameObject> highlightList;
        if (reference.TryGetValue(gameObject, out highlightList))
        {
            GameObject highlighter;
            if(highlightList.TryGetValue(type, out highlighter))
            {
                highlighter.GetComponent<Highlighter>().Stop();
                m_Pools[(int)type].Deactivate(highlighter);
                highlightList.Remove(type);
            }
            if(highlightList.Count == 0)
                reference.Remove(gameObject);
        }
    }
}
