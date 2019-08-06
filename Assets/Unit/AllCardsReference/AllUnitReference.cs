using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Reference/AllUnits")]
public class AllUnitReference : SingletonScriptableObject<AllUnitReference>
{
    public CardCollection m_Warriors;
    public CardCollection m_Spirits;

    public UnitData GetCardById(int id, bool isWarrior = false)
    {
        CardCollection collection = isWarrior ? m_Warriors : m_Spirits;

        return collection.m_CardList[id];
    }

    public UnitData GetCardByName(string name, bool isWarrior = false)
    {
        CardCollection collection = isWarrior ? m_Warriors : m_Spirits;

        foreach(UnitData data in collection.m_CardList)
        {
            if (data.UnitName == name)
                return data;
        }
        return null;
    }
}
