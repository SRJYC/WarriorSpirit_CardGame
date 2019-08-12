using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Unit/Collection")]
public class CardCollection : ScriptableObject
{
    public UnitData m_Warrior;
    public List<UnitData> m_CardList;

    public List<UnitData> GetAllCardsWithCondition(SingleUnitCondition condition)
    {
        List<UnitData> copy = new List<UnitData>();
        copy.AddRange(m_CardList);

        for(int i=copy.Count -1; i>=0; i--)
        {
            if (!condition.Check(copy[i]))
                copy.RemoveAt(i);
        }

        return copy;
    }

    public List<UnitData> GetRandomCardsWithCondition(SingleUnitCondition condition, int num, bool repeat = false)
    {
        List<UnitData> list = GetAllCardsWithCondition(condition);

        return RandomPickFromList.RandomSelect<UnitData>(list, num, repeat);
    }

    public List<UnitData> GetRandomCards(int num, bool repeat = false)
    {
        return RandomPickFromList.RandomSelect<UnitData>(m_CardList, num, repeat);
    }
}
