using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityInfoGetter : ScriptableObject
{
    [Tooltip("Is this automatically done? Used to back track to last player selction.")]
    public bool m_Automatic = true;
    [HideInInspector] public bool m_Done = false; 
    public abstract void GetInfo(Unit source);

    public abstract void StoreInfo(AbilityInfo info);
}
