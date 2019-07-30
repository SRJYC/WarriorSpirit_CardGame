using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityInfoGetter/SourceInfoGetter")]
public class SourceInfoGetter : AbilityInfoGetter
{
    public Unit m_Source { get; private set; }

    public override void GetInfo(Unit unit)
    {
        m_Done = false;

        m_Source = unit;

        m_Done = true;
    }

    public override void StoreInfo(AbilityInfo info)
    {
        SourceInfo sourceInfo = info.CastInfoType<SourceInfo>();

        sourceInfo.m_Source = m_Source;
    }
}
