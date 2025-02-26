﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTurnEndBehavior : StateMachineBehaviour
{
    public UnitEvent unitTurnEndEvent;

    public string turnEndTrigger;
    public string unitTurnStartTrigger;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //get current unit
        Unit unit = OrderManager.Instance.CurrentUnit;

        SingleUnitData data = new SingleUnitData();
        data.m_Unit = unit;
        unitTurnEndEvent.Trigger(unit, data);

        unit = OrderManager.Instance.NextUnit();

        NextState(animator, unit);
    }

    private void NextState(Animator animator, Unit unit)
    {
        if (unit == null)
            animator.SetTrigger(turnEndTrigger);
        else
            animator.SetTrigger(unitTurnStartTrigger);
    }
}
