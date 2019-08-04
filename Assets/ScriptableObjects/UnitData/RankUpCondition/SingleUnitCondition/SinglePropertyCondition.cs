using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SinglePropertyCondition
{
    public enum Compare
    {
        lessThan,
        greaterThan,
        equalTo,
        lessAndEqual,
        greaterAndEqual
    }

    [Header("Spirit's Property must Satisfy")]
    public UnitStatsProperty property;
    public Compare compare;
    public int value;
    
    public bool Check(UnitData data)
    {
        int dataValue = data.GetStat(property);

        switch(compare)
        {
            case Compare.equalTo:
                return dataValue == value;
            case Compare.greaterAndEqual:
                return dataValue >= value;
            case Compare.greaterThan:
                return dataValue > value;
            case Compare.lessAndEqual:
                return dataValue <= value;
            case Compare.lessThan:
                return dataValue < value;
        }
        return false;
    }

    public override string ToString()
    {
        string result = "Stat: "+property.ToString();
        switch (compare)
        {
            case Compare.equalTo:
                result += " = ";
                break;
            case Compare.greaterAndEqual:
                result += " >= ";
                break;
            case Compare.greaterThan:
                result += " > ";
                break;
            case Compare.lessAndEqual:
                result += " <= ";
                break;
            case Compare.lessThan:
                result += " < ";
                break;
        }
        result += value.ToString();
        return result;
    }
}
