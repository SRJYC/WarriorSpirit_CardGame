using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Other/ManaObject")]
public class ManaObject : ScriptableObject
{
    public int limit;
    public int defaultIncrease;

    [Header("Start")]
    public int currentMana;
    public int maxMana;

    public void Increase()
    {
        if(maxMana < limit)
            maxMana += defaultIncrease;
    }

    public void Reset()
    {
        currentMana = maxMana;
    }

    public void Cost(int n)
    {
        if (n > currentMana)
            return;

        currentMana -= n;
    }
}
