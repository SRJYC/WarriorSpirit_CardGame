using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Reference/StatsDisplay")]
public class StatsDisplayReference : SingletonScriptableObject<StatsDisplayReference>
{
    [Tooltip("Properties in this list will not display.")]
    public List<UnitStatsProperty> hideProperties;

    [Tooltip("Properties in this list represent boolean and will display differently.")]
    public List<UnitStatsProperty> boolProperties;

    [Tooltip("The tooltip will be displayed when player hover on corresponding property." +
        "\nIt should be same size as properties")]
    public List<string> tooltipList;
}
