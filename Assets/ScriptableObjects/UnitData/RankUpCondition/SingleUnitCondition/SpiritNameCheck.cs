using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpiritNameCheck
{
    public UnitData m_Unit;
    public bool Check(UnitData data)
    {
        if(m_Unit == null)
            return true;
        else
            return m_Unit.UnitName == data.UnitName;
    }

    public override string ToString()
    {
        return m_Unit != null ? m_Unit.UnitName : "";
    }
}
