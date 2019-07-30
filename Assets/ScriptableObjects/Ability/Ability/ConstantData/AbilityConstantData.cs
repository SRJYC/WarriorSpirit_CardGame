using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Ability/ConstantData")]
public class AbilityConstantData : ScriptableObject
{
    public string m_AbilityName;

    [TextArea(1,10)]
    public string m_Description;
}
