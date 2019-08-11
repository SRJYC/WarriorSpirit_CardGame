using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : ScriptableObject
{
    public int value;
    public virtual void TakeEffect(Unit unit, int power = 0, GameEventData eventData = null)
    {
        value = power;
    }
}
