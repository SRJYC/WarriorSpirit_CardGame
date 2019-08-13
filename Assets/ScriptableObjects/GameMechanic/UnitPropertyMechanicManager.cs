using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameMechanic/Manager", order = 1)]
public class UnitPropertyMechanicManager : ScriptableObject
{
    [SerializeField]private List<UnitPropertyMechanic> mechanics = new List<UnitPropertyMechanic>();

    public void RegisterUnit(Unit unit)
    {
        foreach(var mechanic in mechanics)
        {
            mechanic.Register(unit);
        }
    }

    public void UnregisterUnit(Unit unit)
    {
        foreach (var mechanic in mechanics)
        {
            mechanic.Unregister(unit);
        }
    }
}
