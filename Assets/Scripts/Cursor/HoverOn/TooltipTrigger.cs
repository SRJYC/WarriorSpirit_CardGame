using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : HoverOnTrigger
{
    [TextArea(1,4)]
    public string m_Tooltip = "";
    // Start is called before the first frame update
    void Start()
    {
    }

    protected override void TriggerEnterEvent()
    {
        TooltipData data = new TooltipData();
        data.m_Message = m_Tooltip;
        data.m_Switch = true;

        m_Event.Trigger(data);
    }

    protected override void TriggerExitEvent()
    {
        TooltipData data = new TooltipData();
        data.m_Switch = false;

        m_Event.Trigger(data);
    }
}
