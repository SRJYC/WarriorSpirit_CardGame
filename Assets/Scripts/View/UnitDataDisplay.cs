using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class UnitDataDisplay : MonoBehaviour
{
    public abstract void Display(UnitData data);
}
