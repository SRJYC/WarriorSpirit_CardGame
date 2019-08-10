using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayCondition : MonoBehaviour
{
    public TextMeshProUGUI m_Text1;
    public TextMeshProUGUI m_Text2;

    public GameObject m_ScaficeIcon;

    public void Display(RankUpCondition condition)
    {
        if(condition == null)
        {
            m_ScaficeIcon.SetActive(false);
            m_Text1.text = "N/A";
            m_Text2.text = "N/A";
            return;
        }

        m_ScaficeIcon.SetActive(condition.m_Sacrifice);

        m_Text1.text = condition.GetStringOfCondition1();
        m_Text2.text = condition.GetStringOfCondition2();
    }
}
