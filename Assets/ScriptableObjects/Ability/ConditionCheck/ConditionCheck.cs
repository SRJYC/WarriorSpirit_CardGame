using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionCheck : ScriptableObject
{
    protected AbilityInfo[] m_InfoList;


    /// <summary>
    /// Check if the condition meet
    /// </summary>
    /// <returns></returns>
    public virtual bool Check(AbilityInfo[] infos)
    {
        m_InfoList = infos;
        return true;
    }

    protected T GetAbilityInfo<T>() where T : AbilityInfo
    {
        for (int i = 0; i < m_InfoList.Length; i++)
        {
            if (m_InfoList[i].GetType() == typeof(T))
                return (T)m_InfoList[i];
        }

        Debug.LogError("Effect doesn't get required info object.");
        return null;
    }

}
