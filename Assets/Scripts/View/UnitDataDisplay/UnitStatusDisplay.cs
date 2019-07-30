using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UnitStatusDisplay : UnitDataDisplay
{
    public TextMeshProUGUI m_Text;

    public override void Display(UnitData data)
    {
        if (data.m_Owner == null)
        {
            m_Text.text = "";
            return;
        }

        List<Status> statuses = data.m_Owner.m_Status.GetAllStatus();

        string result = "";
        foreach(Status status in statuses)
        {
            result += StatusDescription(status);
        }

        m_Text.text = result;
    }

    private string StatusDescription(Status status)
    {
        string result= "[" + status.m_StatusName + "]";

        //duration
        if (status.m_Duration > 0)
        {
            result += "(" + status.m_Duration.ToString() + ")";
        }

        result += ":" + status.m_Description+"\n";
        return result;
    }
}
