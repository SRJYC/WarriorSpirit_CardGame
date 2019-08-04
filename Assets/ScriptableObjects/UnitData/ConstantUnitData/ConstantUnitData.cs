using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Part of unit data which won't change during runtime,
//so all instance will reference same scriptable object
[CreateAssetMenu(menuName = "Unit/ConstantData")]
public class ConstantUnitData : ScriptableObject
{
    [Header("Base Info")]
    public string m_UnitName;
    [TextArea(3, 10)] public string m_Description;
    public Sprite m_Artwork;

    [Header("Card Type")]
    public bool m_IsWarrior;

    [Header("Position")]
    public bool m_CanBeFront;
    public bool m_CanBeBack;

    [Header("Power Scale")]
    public bool m_Unique;
    public int m_Rank;

    [Header("Rank Up Options")]
    public List<RankUpCondition> m_RankUps = new List<RankUpCondition>();
}
