using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// listen to state change event and turn on or off controlled component 
/// </summary>
public class StateControlComponent : StateControlObject
{
    public MonoBehaviour m_ControlledComponent;

    // Start is called before the first frame update
    void Start()
    {
        m_StateChangeEvent.RegisterListenner(StateChange);
    }

    private void OnDestroy()
    {
        m_StateChangeEvent.UnregisterListenner(StateChange);
    }

    protected override void StateChange(GameEventData eventData)
    {
        if (m_ControlledComponent == null)
            return;

        StateChangeData data = eventData.CastDataType<StateChangeData>();
        if (data == null)
            return;

        bool check = CheckState(data.currentStates);
        bool active = m_ControlledComponent.enabled;
        if (check ^ active)
        {
            m_ControlledComponent.enabled = !active;
        }
    }
}
