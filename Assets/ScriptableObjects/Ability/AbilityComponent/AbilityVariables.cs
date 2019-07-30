using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityVariables
{
    Dictionary<AbilityVariableField, int> m_VariableDictionary = new Dictionary<AbilityVariableField, int>();

    public void ChangeVariable(AbilityVariableField field, int value)
    {
        int v;
        if (m_VariableDictionary.TryGetValue(field, out v))
        {
            Debug.Log("before change:" + m_VariableDictionary[field]);
            v += value;
            Debug.Log("after change:" + m_VariableDictionary[field]);
        }
        else
            m_VariableDictionary.Add(field, value);
    }

    public void SetVariable(AbilityVariableField field, int value)
    {
        int v;
        if (m_VariableDictionary.TryGetValue(field, out v))
        {
            Debug.Log("before set:" + m_VariableDictionary[field]);
            v = value;
            Debug.Log("after set:" + m_VariableDictionary[field]);
        }
        else
            m_VariableDictionary.Add(field, value);
    }

    public int GetVariable(AbilityVariableField field)
    {
        int v;
        if (m_VariableDictionary.TryGetValue(field, out v))
        {
            Debug.Log("exist:" + m_VariableDictionary[field]);
        }
        else
            Debug.Log("not exist");

        return v;
    }
}
