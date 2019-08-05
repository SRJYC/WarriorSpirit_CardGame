using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : HoverOnTrigger
{
    public TextProperty m_text;
    [TextArea(1,4)]
    public string m_Tooltip = "";
    // Start is called before the first frame update
    void Start()
    {
    }

    protected override void TriggerEnterEvent()
    {
        TooltipData data = new TooltipData();

        if (m_text == null)
            data.m_Message = m_Tooltip;
        else
            data.m_Message = m_text.ToString();

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
