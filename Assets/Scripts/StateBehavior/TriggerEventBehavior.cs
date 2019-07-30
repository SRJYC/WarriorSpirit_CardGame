using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trigger event and then move to next state
/// </summary>
public class TriggerEventBehavior : StateMachineBehaviour
{
    [Header("Event To Trigger")]
    public GameEvent m_Event;

    //[Header("Exist Trigger")]
    //public string parameter;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(m_Event != null)
        {
            m_Event.SetActive(true);
            m_Event.Trigger();
        }

        //animator.SetTrigger(parameter);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
