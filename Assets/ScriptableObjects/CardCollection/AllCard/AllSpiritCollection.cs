using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/AllSpirit")]
public class AllSpiritCollection: ScriptableObject
{
    public List<UnitData> m_Spirits;

    private Dictionary<UnitData, List<UnitData>> m_OriginSpirits;

    public void OnEnable()
    {
        m_OriginSpirits = new Dictionary<UnitData, List<UnitData>>();

        foreach (UnitData data in m_Spirits)
        {
            foreach(RankUpCondition condition in data.m_RankUps)
            {
                UnitData highRankUnit = condition.m_HighRankSpirit;

                List<UnitData> units;
                if (m_OriginSpirits.TryGetValue(highRankUnit, out units))
                {
                    if (!units.Contains(data))
                        units.Add(data);
                }
                else
                {
                    units = new List<UnitData>();
                    units.Add(data);
                    m_OriginSpirits.Add(highRankUnit, units);
                }
            }
        }
    }

    public List<UnitData> GetOriginSpirits(UnitData data)
    {
        List<UnitData> units;
        if (m_OriginSpirits.TryGetValue(data, out units))
        {
            return units;
        }
        return new List<UnitData>();
    }
}
