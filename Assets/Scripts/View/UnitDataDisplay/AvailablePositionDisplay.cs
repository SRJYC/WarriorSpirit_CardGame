using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailablePositionDisplay : UnitDataDisplay
{
    public Image m_FrontlineIcon;
    public Image m_BacklineIcon;

    public override void Display(UnitData data)
    {
        m_FrontlineIcon.enabled = data.CanBeFrontline;
        m_BacklineIcon.enabled = data.CanBeBackline;
    }
}
