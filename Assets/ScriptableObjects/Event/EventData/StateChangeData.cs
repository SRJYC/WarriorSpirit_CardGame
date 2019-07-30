using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChangeData : GameEventData
{
    public string changedStateLayer;
    public StateID changedState;
    public List<StateID> currentStates;
}
