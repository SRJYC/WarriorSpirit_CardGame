using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatsDisplay : UnitDataDisplay
{
    public int m_Columns = 1;
    public int m_Row = -1;
    public GameObject m_PropertyBlock;

    private Stack<GameObject> m_Actives = new Stack<GameObject>();
    private Stack<GameObject> m_Inactives = new Stack<GameObject>();

    [SerializeField] private Vector3 changeOfScale;
    [SerializeField] private Vector3 originalScale = Vector3.one;
    [SerializeField] private Vector2 requireDimension = new Vector2(175,50);

    public override void Display(UnitData data)
    {
        ResetObject();

        Dictionary<UnitStatsProperty, int> stats = data.GetAllStat();

        int n = stats.Count;
        if (m_Row >= 0 && m_Row < stats.Count)
            n = m_Row;

        ScaleInfoPanel(n);

        InitProperties(stats, n);
    }

    private void ScaleInfoPanel(int n)
    {
        int x = m_Columns;
        int y = Mathf.CeilToInt((float)n / (float)x);

        changeOfScale = new Vector3(x, y, 1);
        this.transform.localScale = new Vector3(originalScale.x * changeOfScale.x, originalScale.y * changeOfScale.y, originalScale.z * changeOfScale.z);

        GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
        grid.constraintCount = m_Columns;
        grid.cellSize = requireDimension / new Vector2(x, y);
    }

    private void InitProperties(Dictionary<UnitStatsProperty, int> stats, int n)
    {
        int count = 0;
        foreach (KeyValuePair<UnitStatsProperty, int> pair in stats)
        {
            if (count >= n)
                break;

            if (StatsDisplayReference.Instance.hideProperties.Contains(pair.Key))
                continue;

            GameObject property = GetPropertyBlock();

            property.SetActive(true);
            m_Actives.Push(property);

            property.transform.SetParent(this.transform);
            property.transform.localScale = new Vector3(1.0f / changeOfScale.x, 1.0f / changeOfScale.y, 1.0f / changeOfScale.z);

            property.GetComponent<SinglePropertyDisplay>().Display(pair);

            count++;
        }
    }

    private GameObject GetPropertyBlock()
    {
        if (m_Inactives.Count > 0)
            return m_Inactives.Pop();
        else
            return Instantiate(m_PropertyBlock);
    }

    private void OnDisable()
    {
        ResetObject();
    }

    private void ResetObject()
    {
        this.transform.localScale = originalScale;

        while (m_Actives.Count > 0)
        {
            GameObject property = m_Actives.Pop();
            property.SetActive(false);
            m_Inactives.Push(property);
        }
    }

    [ContextMenu("Reset and Destroy Child")]
    private void OnDestroy()
    {
        ResetObject();

        while(m_Inactives.Count > 0)
        {
            GameObject property = m_Inactives.Pop();
            DestroyImmediate(property);
        }
    }


    [ContextMenu("Test")]
    private void Test()
    {
        Dictionary<UnitStatsProperty, int> stats = new Dictionary<UnitStatsProperty, int>();
        stats.Add(UnitStatsProperty.POW, 0);
        stats.Add(UnitStatsProperty.Formation, 1);
        stats.Add(UnitStatsProperty.Charged, 1);

        ScaleInfoPanel(stats.Count);

        InitProperties(stats,stats.Count);
    }
}
