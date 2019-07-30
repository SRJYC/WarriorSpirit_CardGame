using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassButton : MonoBehaviour
{
    public GameEvent m_PassEvent;
    public void Trigger()
    {
        m_PassEvent.Trigger();
    }
}
