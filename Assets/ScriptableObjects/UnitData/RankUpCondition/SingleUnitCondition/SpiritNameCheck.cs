using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpiritNameCheck
{
    public UnitData m_Unit;
    public bool Check(UnitData data)
    {
        return m_Unit.UnitName == data.UnitName;
    }

    public override string ToString()
    {
        string result = "Require Spirit: " + m_Unit.UnitName;
        return result;
    }
}
