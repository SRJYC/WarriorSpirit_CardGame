﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventRespond : MonoBehaviour
{
    public GameEvent m_receivedEvent;
    public GameEvent m_respondEvent;

    // Start is called before the first frame update
    void Start()
    {
        m_receivedEvent.RegisterListenner(Respond);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Respond(GameEventData eventData)
    {
        Invoke("t", 1);
    }

    void t()
    {
        m_respondEvent.Trigger();
    }
}