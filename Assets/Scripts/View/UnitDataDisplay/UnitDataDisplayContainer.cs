using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataDisplayContainer : UnitDataDisplay
{
    public List<UnitDataDisplay> list = new List<UnitDataDisplay>();

    [SerializeField]private UnitData testData = null;
    public override void Display(UnitData data)
    {
        foreach(UnitDataDisplay display in list)
        {
            display.Display(data);
        }
    }

    [ContextMenu("test")]
    public void Test()
    {
        Display(testData);
    }
}
