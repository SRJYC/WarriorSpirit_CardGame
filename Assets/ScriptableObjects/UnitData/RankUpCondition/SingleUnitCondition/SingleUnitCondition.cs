using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RankUpConditionCheck/SingleUnit")]
public class SingleUnitCondition : ScriptableObject
{
    public RankUpTypeCheck[] typeChecks;
    public SinglePropertyCondition[] propertyConditions;

    public bool Check(UnitData data)
    {
        return CheckType(data) && CheckProperty(data);
    }

    public override string ToString()
    {
        string result = "";

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

    [ContextMenu("Test")]
    public void Test()
    {
        Debug.Log(this.ToString());
    }
}
