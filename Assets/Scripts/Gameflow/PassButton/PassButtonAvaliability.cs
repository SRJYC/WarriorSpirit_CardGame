using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PassButtonAvaliability : MonoBehaviour
{
    public GameEvent m_SwitchOnEvent;
    public GameEvent m_SwitchOffEvent;

    // Start is called before the first frame update
    void Start()
    {
        if(m_SwitchOnEvent != null)
            m_SwitchOnEvent.RegisterListenner(SwitchOn);

        if (m_SwitchOffEvent != null)
            m_SwitchOffEvent.RegisterListenner(SwitchOff);

        SwitchOff(null);
    }

    void SwitchOn(GameEventData data)
    {
        Debug.Log("Turn On");
        gameObject.SetActive(true);
    }

    void SwitchOff(GameEventData data)
    {
        Debug.Log("Turn Off");
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (m_SwitchOnEvent != null)
            m_SwitchOnEvent.UnregisterListenner(SwitchOn);

        if (m_SwitchOffEvent != null)
            m_SwitchOffEvent.UnregisterListenner(SwitchOff);
    }
}
