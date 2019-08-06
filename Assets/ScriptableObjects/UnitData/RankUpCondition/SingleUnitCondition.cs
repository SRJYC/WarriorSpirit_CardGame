using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SingleUnitCondition
{
    [Header("Need Specific Spirit")]
    public SpiritNameCheck nameCheck;
    [Header("Need Specific Type")]
    public RankUpTypeCheck[] typeChecks;
    [Header("Need Specific Property")]
    public SinglePropertyCondition[] propertyConditions;

    public bool Check(UnitData data)
    {
        return CheckName(data) && CheckType(data) && CheckProperty(data);
    }

    public override string ToString()
    {
        string result = "";

        result += "<b>"+nameCheck.ToString() + "</b>\n";

        foreach (RankUpTypeCheck condition in typeChecks)
        {
            result += condition.ToString() + "\n";
        }

        foreach (SinglePropertyCondition condition in propertyConditions)
        {
            result += condition.ToString() + "\n";
        }

        return result;
    }

    private bool CheckName(UnitData data)
    {
        return nameCheck.Check(data);
    }

    private bool CheckType(UnitData data)
    {
        foreach (RankUpTypeCheck condition in typeChecks)
        {
            if (!condition.Check(data))
                return false;
        }
        return true;
    }

    private bool CheckProperty(UnitData data)
    {
        foreach(SinglePropertyCondition condition in propertyConditions)
        {
            if (!condition.Check(data))
                return false;
        }
        return true;
    }
}
