using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ScriptableObject
{

    protected AbilityInfo[] m_InfoList;

    public virtual void TakeEffect(AbilityInfo[] info)
    {
        m_InfoList = info;
    }

    protected T GetAbilityInfo<T>() where T:AbilityInfo
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
