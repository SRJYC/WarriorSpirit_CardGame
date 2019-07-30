using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ManaDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TooltipTrigger tooltip;

    private ManaObject mana;

    public void Init(ManaObject manaObject)
    {
        mana = manaObject;

        tooltip.m_Tooltip = "Increase Maximum amount by "+mana.defaultIncrease+" at turn start each turn until reach the limit "+mana.limit;
    }

    private void Update()
    {
        if(mana != null)
        {
            text.text = mana.currentMana + "/" + mana.maxMana;
        }
    }
}
