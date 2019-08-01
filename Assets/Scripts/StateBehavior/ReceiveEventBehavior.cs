using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Listen to that event, when triggered, move to next state
/// </summary>
public class ReceiveEventBehavior : StateMachineBehaviour
{
    [Header("Event To Listen")]
    public GameEvent m_Event;

    [Header("Exist Trigger")]
    public string parameter;

    public Animator m_Animator;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_Event != null)
        {
            m_Event.RegisterListenner(Trigger);
        }

        m_Animator = animator;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_Event != null)
            m_Event.UnregisterListenner(Trigger);
    }

    private void Trigger(GameEventData data)
    {
        m_Animator.SetTrigger(parameter);
    }
}
