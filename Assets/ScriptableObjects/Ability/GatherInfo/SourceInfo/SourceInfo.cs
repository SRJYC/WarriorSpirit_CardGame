using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/AbilityInfo/Source")]
public class SourceInfo : AbilityInfo
{
    public Unit m_Source { get; set; }

    public int GetPower()
    {
        return -1;
    }
}
