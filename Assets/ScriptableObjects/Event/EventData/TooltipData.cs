using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Event data which contains one string as message, and a switch.
/// </summary>
public class TooltipData : GameEventData
{
    public bool m_Switch;
    public string m_Message;
}
