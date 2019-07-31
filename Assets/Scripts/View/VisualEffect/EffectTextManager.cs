using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTextManager : MonoBehaviour
{
    public GameObject m_prefab;
 
    public GameEvent m_StatsChangeEvent;

    public float m_DisplayTime;

    public Color m_PositiveColor;
    public Color m_NegativeColor;

    private ObjectPool m_Pool;
    private void Start()
    {
        m_StatsChangeEvent.RegisterListenner(Display);

        m_Pool = new ObjectPool(m_prefab);
    }

    private void OnDestroy()
    {
        m_StatsChangeEvent.UnregisterListenner(Display);
    }

    public void Display(GameEventData eventData)
    {
        //Debug.Log("Received");
        UnitStatsChangeData data = eventData.CastDataType<UnitStatsChangeData>();
        if (data == null)
            return;

        Unit unit = data.m_Unit;
        int delta = data.m_Delta;
        string property = data.m_ChangedStats.ToString();

        string op = delta >= 0 ? " +" : " ";
        string text = property + op + delta;

        Color color = delta < 0 ? m_NegativeColor : m_PositiveColor;

        GameObject go = m_Pool.Get(true);
        go.transform.SetParent(this.transform);
        go.GetComponent<EffectText>().Display(unit.transform.position,text,color);

        StartCoroutine(Hide(go));
    }

    IEnumerator Hide(GameObject gameObject)
    {
        yield return new WaitForSeconds(m_DisplayTime);
        m_Pool.Deactivate(gameObject, true);
    }
}
