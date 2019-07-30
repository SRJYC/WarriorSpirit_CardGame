using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// listen to state change event and turn on or off attached game object 
/// </summary>
public class StateControlObject : MonoBehaviour
{
    public GameEvent m_StateChangeEvent;

    [Header("Enable Component in These States")]
    public List<StateID> m_AvaliableState;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Register State Change event");
        m_StateChangeEvent.RegisterListenner(StateChange);
    }

    private void OnDestroy()
    {
        m_StateChangeEvent.UnregisterListenner(StateChange);
    }

    protected virtual void StateChange(GameEventData eventData)
    {
        //Debug.Log("Receive State Change Event");
        StateChangeData data = eventData.CastDataType<StateChangeData>();
        if (data == null)
            return;
       
        bool check = CheckState(data.currentStates);
        bool active = gameObject.activeSelf;
        if (check ^ active)
        {
            gameObject.SetActive(!active);
        }
    }

    protected bool CheckState(List<StateID> currentStates)
    {
        foreach (StateID state in m_AvaliableState)
        {
            if (currentStates.Contains(state))
                return true;
        }
        return false;
    }
}
