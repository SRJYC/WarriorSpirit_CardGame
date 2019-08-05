using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SinglePropertyDisplay : MonoBehaviour
{
    public TextMeshProUGUI m_Label;
    public TextMeshProUGUI m_Value;

    public Toggle m_bool;

    public TooltipTrigger tooltip;

    public void Display(KeyValuePair<UnitStatsProperty,int> pair)
    {
        Display(pair.Key, pair.Value);
    }

    public void Display(UnitStatsProperty statsProperty, int value)
    {
        if(StatsDisplayReference.Instance.boolProperties.Contains(statsProperty))
        {
            m_Value.gameObject.SetActive(false);

            m_bool.Display(value > 0);
        }
        else
        {
            m_bool.gameObject.SetActive(false);

            m_Value.text = value.ToString();
        }

        m_Label.text = AllTextEnumProxy.Instance.GetText(statsProperty);

        ChangeTootltip(statsProperty);
    }

    private void ChangeTootltip(UnitStatsProperty statsProperty)
    {
        //Debug.Log("Change Tooltip");
        if (tooltip == null)
            return;

        string tip = StatsDisplayReference.Instance.tooltipList[(int)statsProperty];
        //Debug.Log("Get Tooltip["+statsProperty.ToString()+"]["+ (int)statsProperty + "]:["+tip+"]");

        if (tip == "")
            tooltip.enabled = false;
        else
        {
            tooltip.enabled = true;
            tooltip.m_Tooltip = tip;
        }
    }


    [ContextMenu("test")]
    private void Test()
    {
        Display((UnitStatsProperty)Random.Range(1,9), Random.Range(0,999));
    }
}
