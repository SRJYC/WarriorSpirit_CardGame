using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RankUpTypeCheck
{
    [Header("Spirit needs to be one of the following types")]
    public SpiritType[] spiritTypes = new SpiritType[0];
    
    public bool Check(UnitData data)
    {
        foreach (SpiritType type in spiritTypes)
        {
            if (data.IsSpiritType(type))
                return true;
        }
        return false;
    }

    public override string ToString()
    {
        string result = "Type: ";
        string connect = " or ";
        for (int i = 0; i < spiritTypes.Length; i++)
        {
            result += spiritTypes[i].ToString();
            if (i != spiritTypes.Length - 1)
                result += connect;
        }
        return result;
    }
}
