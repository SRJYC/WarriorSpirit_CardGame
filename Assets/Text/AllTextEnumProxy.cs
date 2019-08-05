using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text/EnumProxy")]
public class AllTextEnumProxy : SingletonScriptableObject<AllTextEnumProxy>
{
    [ContextMenu("Write")]
    public void Write()
    {
        AllText allText = AllText.Instance;
        if (allText == null)
        {
            return;
        }

        WriteStatsProperty(allText);

        WriteSpiritType(allText);
    }

    private static void WriteSpiritType(AllText allText)
    {
        foreach (SpiritType property in System.Enum.GetValues(typeof(SpiritType)))
        {
            allText.AddText(property.ToString(), property.ToString());
        }
    }

    private static void WriteStatsProperty(AllText allText)
    {
        foreach (UnitStatsProperty property in System.Enum.GetValues(typeof(UnitStatsProperty)))
        {
            allText.AddText(property.ToString(), property.ToString());
        }
    }

    public string GetText(UnitStatsProperty property)
    {
        AllText allText = AllText.Instance;
        if(allText != null)
        {
            return allText.GetText(property.ToString());
        }
        else
        {
            return property.ToString();
        }
    }

    public string GetText(SpiritType property)
    {
        AllText allText = AllText.Instance;
        if (allText != null)
        {
            return allText.GetText(property.ToString());
        }
        else
        {
            return property.ToString();
        }
    }
}
