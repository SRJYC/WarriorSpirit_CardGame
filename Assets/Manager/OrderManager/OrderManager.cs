using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : Singleton<OrderManager>
{
    [Header("Game State/Phase Event")]
    public GameEvent m_OrderInitEvent;
    public GameEvent m_EndOrderInitEvent;

    [Header("Game Object")]
    public GameObject m_Container;

    [Header("Prefab")]
    public GameObject m_MarkObject;
    private ObjectPool m_Pool;

    [Header("Game End Event")]
    public GameEvent m_WarriorDestroyEvent;

    [Header("Unit Property Mechanic")]
    public UnitPropertyMechanicManager m_MechanicManager;

    private List<Unit> m_SortedUnitList;
    private List<Unit> m_UnsortedUnitList;
    private Dictionary<Unit, GameObject> m_UnitMarkMap;

    private Unit m_CurrentUnit;
    private int m_NextUnitIndex;
    public Unit CurrentUnit { get { return m_CurrentUnit; } }

    // Start is called before the first frame update
    void Start()
    {
        m_Pool = new ObjectPool(m_MarkObject);
        m_SortedUnitList = new List<Unit>();
        m_UnsortedUnitList = new List<Unit>();
        m_UnitMarkMap = new Dictionary<Unit, GameObject>();

        if (m_OrderInitEvent != null)
            m_OrderInitEvent.RegisterListenner(TurnStartInit);
    }

    private void OnDestroy()
    {
        if (m_OrderInitEvent != null)
            m_OrderInitEvent.UnregisterListenner(TurnStartInit);
    }

    /// <summary>
    /// Move Current to next and return current unit
    /// </summary>
    /// <returns></returns>
    public Unit NextUnit()
    {
        AuraManager.Instance.RefreshAura(m_UnsortedUnitList);

        if (m_CurrentUnit != null)
            m_UnitMarkMap[m_CurrentUnit].GetComponent<OrderMarkDisplay>().Resize();

        if (m_NextUnitIndex >= m_SortedUnitList.Count)
        {
            m_CurrentUnit = null;
            return null;
        }

        m_CurrentUnit = m_SortedUnitList[m_NextUnitIndex];
        m_UnitMarkMap[m_CurrentUnit].GetComponent<OrderMarkDisplay>().Enlarge();

        m_NextUnitIndex++;

        return m_CurrentUnit;
    }

    private void TurnStartInit(GameEventData data)
    {
        SortList();

        foreach(GameObject mark in m_UnitMarkMap.Values)
        {
            mark.GetComponent<OrderMarkDisplay>().SetActiveColor(true);
        }

        if (m_SortedUnitList.Count > 0)
        {
            m_CurrentUnit = m_SortedUnitList[0];
            m_UnitMarkMap[m_CurrentUnit].GetComponent<OrderMarkDisplay>().Enlarge();

            m_NextUnitIndex = 1;
        }
        else
            m_CurrentUnit = null;


        AuraManager.Instance.RefreshAura(m_UnsortedUnitList);

        //Debug.Log("Order Manager Trigger ");
        m_EndOrderInitEvent.Trigger();
    }

    public void AddUnit(Unit unit)
    {
        m_UnsortedUnitList.Add(unit);

        GameObject mark = m_Pool.Get(true);
        bool ally = GetAllyEnemy(unit);

        OrderMarkDisplay markDisplay = mark.GetComponent<OrderMarkDisplay>();
        markDisplay.Display(unit, ally);
        markDisplay.SetActiveColor(false);

        mark.transform.SetParent(m_Container.transform);
        mark.transform.SetSiblingIndex(m_SortedUnitList.IndexOf(unit));

        m_UnitMarkMap.Add(unit, mark);

        m_MechanicManager.RegisterUnit(unit);
        AuraManager.Instance.RefreshAura(m_UnsortedUnitList);
    }

    public void RemoveUnit(Unit unit)
    {
        if(m_CurrentUnit == unit)
        {
            m_CurrentUnit = null;
        }

        if(m_SortedUnitList.IndexOf(unit) < m_NextUnitIndex)
        {
            m_NextUnitIndex--;
        }

        m_UnsortedUnitList.Remove(unit);
        m_SortedUnitList.Remove(unit);

        GameObject mark;
        if (m_UnitMarkMap.TryGetValue(unit, out mark))
        {
            mark.transform.SetParent(null);
            m_Pool.Deactivate(mark, true);

            m_UnitMarkMap.Remove(unit);
        }


        m_MechanicManager.UnregisterUnit(unit);
        AuraManager.Instance.RemoveUnit(unit);

        WarriorDestroy(unit);
    }

    private void WarriorDestroy(Unit unit)
    {
        if (unit.m_Data.IsWarrior)
        {
            SingleUnitData data = new SingleUnitData();
            data.m_Unit = unit;

            m_WarriorDestroyEvent.Trigger(data);
        }
    }

    private bool GetAllyEnemy(Unit unit)
    {
        return PlayerManager.Instance.GetLocalPlayerID() == unit.m_PlayerID;
    }

    private void SortList()
    {
        m_SortedUnitList = m_UnsortedUnitList.OrderByDescending(unit => unit.m_Data.GetStat(UnitStatsProperty.SPD)).ToList();

        for(int i=0; i<m_SortedUnitList.Count; i++)
        {
            m_UnitMarkMap[m_SortedUnitList[i]].transform.SetSiblingIndex(i);
        }
    }
}
