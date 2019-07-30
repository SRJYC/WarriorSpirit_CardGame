using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitTextDisplay : UnitDataDisplay
{
    public TextMeshProUGUI m_NameText;
    public TextMeshProUGUI m_TypeText;
    public TextMeshProUGUI m_DescriptionText;

    public override void Display(UnitData unitData)
    {
        m_NameText.text = unitData.UnitName;
        m_DescriptionText.text = unitData.Description;

        DisplayTypes(unitData);
    }

    private void DisplayTypes(UnitData unitData)
    {
        string typeText = "";
        if (unitData.IsWarrior)
            AddType("Warrior", ref typeText, "color=#e5e4e2", "color");
        foreach (SpiritType type in unitData.GetSpiritTypes())
        {
            AddType(type.ToString(), ref typeText);
        }
        m_TypeText.text = typeText;

    }

    private void AddType(string type, ref string text, string startTag = "", string endTag = "")
    {
        string textToAdd = "";
        if(text.Length > 0)//not first type
        {
            textToAdd = ",";
        }

        bool haveTag = false;
        if(startTag != "")
        {
            textToAdd += "<" + startTag + ">";
            haveTag = true;
        }

        textToAdd += type;

        if(haveTag && endTag == "")
        {
            textToAdd += "</" + startTag + ">";
        }
        else if(haveTag)
        {
            textToAdd += "</" + endTag + ">";
        }

        text += textToAdd;
    }
}
