using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOnCardPreview : HoverOnTrigger
{
    [HideInInspector] public RankUpCondition condition;

    protected override void TriggerEnterEvent()
    {
        Trigger(true);
    }

    protected override void TriggerExitEvent()
    {
        //Trigger(false);
    }

    private void Trigger(bool toggle)
    {
        RankUpConditionData data = new RankUpConditionData();

        data.m_Switch = toggle;
        data.m_Condition = condition;

        m_Event.Trigger(data);
    }
}
