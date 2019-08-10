using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Other/UnitEvent")]
public class UnitEvent : ScriptableObject
{
    [TextArea(1,4)]
    public string description;

    public delegate void ListennerTrigger(GameEventData eventData);

    private Dictionary<Unit, ListennerTrigger> m_Listenners = new Dictionary<Unit, ListennerTrigger>();

    public UnitEvent m_UnitDestroyEvent = null;

    private Dictionary<Unit, List<ListennerTrigger>> ToRemove = new Dictionary<Unit, List<ListennerTrigger>>();
    private List<Unit> UnitToRemove = new List<Unit>();
    [SerializeField]private int chain = 0;

    public void Trigger(Unit unit, GameEventData data = null)
    {
        chain++;
        ListennerTrigger trigger;
        if(m_Listenners.TryGetValue(unit, out trigger))
        {
            trigger?.Invoke(data);
        }

        chain--;

        RemoveListenner();
    }

    public void RegisterListenner(Unit unit, ListennerTrigger newTrigger)
    {
        if (m_Listenners.ContainsKey(unit))
        {
            m_Listenners[unit] += newTrigger;
        }
        else
        {
            ListennerTrigger trigger = null;
            trigger += newTrigger;

            m_Listenners.Add(unit, trigger);

            if(m_UnitDestroyEvent != null)
                m_UnitDestroyEvent.RegisterListenner(unit, UnitDestroy);
        }
    }

    public void UnregisterListenner(Unit unit, ListennerTrigger trigger)
    {
        List<ListennerTrigger> triggerlist;
        if (ToRemove.TryGetValue(unit, out triggerlist))
        {
            triggerlist.Add(trigger);
        }
        else
        {
            triggerlist = new List<ListennerTrigger>();
            triggerlist.Add(trigger);

            ToRemove.Add(unit, triggerlist);
        }

        RemoveListenner();
    }

    private void RemoveListenner()
    {
        if(chain <= 0)
        {
            foreach(KeyValuePair<Unit, List<ListennerTrigger>> pair in ToRemove)
            {
                if(!m_Listenners.ContainsKey(pair.Key))
                    continue;

                foreach(ListennerTrigger trigger in  pair.Value)
                {
                    m_Listenners[pair.Key] -= trigger;
                }
            }

            ToRemove.Clear();

            foreach(Unit unit in UnitToRemove)
            {
                m_Listenners.Remove(unit);
            }

            UnitToRemove.Clear();
        }

    }

    private void UnitDestroy(GameEventData eventData)
    {
        //Debug.Log(this + " Receive Unit Destroy Event");
        SingleUnitData data = eventData.CastDataType<SingleUnitData>();
        if(data != null)
        {
            Unit unit = data.m_Unit;
            UnitToRemove.Add(unit);

            //Debug.Log(this + " chain "+ chain);
            m_UnitDestroyEvent.UnregisterListenner(unit, UnitDestroy);
        }

        RemoveListenner();
    }
}
