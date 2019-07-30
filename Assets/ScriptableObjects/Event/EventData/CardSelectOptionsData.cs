using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectOptionsData : GameEventData
{
    public bool m_Switch;

    public bool isCondition = false;
    public UnitData[] unitDatas;
    public RankUpCondition[] conditions;
}
