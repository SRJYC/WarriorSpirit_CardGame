using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event data which contains a source, a list of targets(unit and block), and an effect which triggered this event
/// </summary>
public class EffectData : GameEventData
{
    public Unit m_Source;
    public List<Unit> m_Targets;
    public List<FieldBlock> m_TargetBlock;

    public Effect m_TriggeredEffect;
}
