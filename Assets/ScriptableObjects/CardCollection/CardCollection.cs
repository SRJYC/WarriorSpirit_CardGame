using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Unit/Collection")]
public class CardCollection : ScriptableObject
{
    public UnitData m_Warrior;
    public List<UnitData> m_CardList;

    public List<UnitData> GetRandomCards(int num, bool repeat = false)
    {
        int size = m_CardList.Count;
        if(size <= num)
        {
            return m_CardList;
        }

        List<UnitData> list = new List<UnitData>();

        List<int> indices = new List<int>();
        for(int i=0; i<num; i++)
        {
            int index = Random.Range(0, size);
            while(!repeat && indices.Contains(index))
            {
                index = Random.Range(0, size);
            }
            indices.Add(index);
        }

        foreach(int i in indices)
        {
            list.Add(m_CardList[i]);
        }

        return list;
    }
}
