using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChangeData : GameEventData
{
    public string changedStateLayer;
    public List<StateID> currentStates;
}
