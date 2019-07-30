﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Other/GameEvent")]
public class GameEvent : ScriptableObject
{
    [TextArea(1, 4)]
    public string description;

    public delegate void ListennerTrigger(GameEventData eventData);
    private ListennerTrigger m_Listenners;

    //private List<GameEventListenner> m_Listenners = new List<GameEventListenner>();

    private List<ListennerTrigger> ToRemove = new List<ListennerTrigger>();
    private int chain = 0;

    //[SerializeField]private bool active = false;

    public void OnEnable()
    {
        m_Listenners = null;
    }

    public void SetActive(bool set)
    {
        //if(active != set)
        //{
         //   Debug.Log("Active [" + set + "] : [" + this + "]");
        //    active = set;
        //}
    }

    public void Trigger(GameEventData eventData = null)
    {
        //Debug.Log("Trigger Event : [" + this + "]");// with Active Status :["+active+"]");

        //if (!active)
        //    return;

        chain++;

        m_Listenners?.Invoke(eventData);

        chain--;

        RemoveListenner();
    }

    public void RegisterListenner(ListennerTrigger trigger)
    {
        m_Listenners += trigger;
    }

    public void UnregisterListenner(ListennerTrigger trigger)
    {
        ToRemove.Add(trigger);
        RemoveListenner();
    }

    private void RemoveListenner()
    {
        if(chain<=0)
        {
            foreach(ListennerTrigger trigger in ToRemove)
            {
                m_Listenners -= trigger;
            }
            ToRemove.Clear();
        }
    }
}