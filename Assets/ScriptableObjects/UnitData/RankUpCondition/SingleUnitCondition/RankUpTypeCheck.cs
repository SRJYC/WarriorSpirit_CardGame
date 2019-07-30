using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RankUpConditionCheck/TypeCheck")]
public class RankUpTypeCheck : ScriptableObject
{
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
        string result = "";
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
