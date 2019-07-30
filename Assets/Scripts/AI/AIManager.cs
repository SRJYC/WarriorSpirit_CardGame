using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : Singleton<AIManager>
{
    public List<UnitData> GetCardChoice(List<UnitData> options, int num = 1, bool repeat = false)
    {
        Debug.Log("AI choose random card from options");

        int size = options.Count;
        if (size <= num)
        {
            return options;
        }

        List<UnitData> list = new List<UnitData>();

        List<int> indices = new List<int>();
        for (int i = 0; i < num; i++)
        {
            int index = Random.Range(0, size);
            while (!repeat && indices.Contains(index))
            {
                index = Random.Range(0, size);
            }
            indices.Add(index);
        }

        foreach (int i in indices)
        {
            list.Add(options[i]);
        }

        return list;
    }
}
