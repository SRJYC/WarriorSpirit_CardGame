using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="RankUpConditionCheck/FullCheck")]
public class RankUpCondition : ScriptableObject
{
    [Header("High Rank Spirit")]
    public UnitData m_HighRankSpirit;
    [Tooltip("With [Scarifice] property, this condition will be checked even the original spirit is in hand(slot 2).")]
    public bool m_Scarifice = false;

    [Header("Spirit 1 / Spirit on Board")]
    public SingleUnitCondition data1Condition = null;
    [Header("Spirit 2 / Spirit on Board")]
    public SingleUnitCondition data2Condition = null;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="data1">The unit on the board</param>
    /// <param name="data2">The unit in the hand</param>
    /// <returns></returns>
    public bool Check(UnitData data1, UnitData data2)
    {
        if(data1Condition != null)
        {
            bool check1 = data1Condition.Check(data1);
            if (!check1)
                return false;
        }

        if(data2Condition != null)
        {
            bool check2 = data2Condition.Check(data2);
            if (!check2)
                return false;
        }

        return true;
    }

    public string GetStringOfCondition1()
    {
        //Debug.Log("data1 ["+data1Condition+"]");
        if (data1Condition != null)
            return data1Condition.ToString();

        return "";
    }

    public string GetStringOfCondition2()
    {
        //Debug.Log("data2 [" + data1Condition + "]");
        if (data2Condition != null)
            return data2Condition.ToString();

        return "";
    }
}
