using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTurnStartBehavior : StateMachineBehaviour
{
    public UnitEvent unitTurnStartEvent;

    public string playerTurnTrigger;
    public string enemyTurnTrigger;
    public string gameEndTrigger;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //get current unit
        Unit unit = OrderManager.Instance.CurrentUnit;
        if (unit == null)
        {
            animator.SetTrigger(gameEndTrigger);
            return;
        }

        unitTurnStartEvent.Trigger(unit);

        NextState(animator, unit);
    }

    private void NextState(Animator animator, Unit unit)
    {
        bool player = PlayerManager.Instance.GetLocalPlayerID() == unit.m_PlayerID;
        if (player)
            animator.SetTrigger(playerTurnTrigger);
        else
            animator.SetTrigger(enemyTurnTrigger);
    }
}
