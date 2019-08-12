using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickFromList
{
    public static List<T> RandomSelect<T>(List<T> list, int num, bool repeat = false)
    {
        int size = list.Count;
        if (size <= num)
        {
            return list;
        }

        List<T> selectlist = new List<T>();

        List<int> indices = new List<int>();
        for (int i = 0; i < num; i++)
        {
            int index = Random.Range(0, size);
            while (!repeat && indices.Contains(index))
            {
                index = Random.Range(0, size);
            }
            indices.Add(index);
            selectlist.Add(list[index]);
        }

        return selectlist;
    }
}
