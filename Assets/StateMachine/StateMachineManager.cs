using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : Singleton<StateMachineManager>
{
    public Dictionary<string, StateMachine> states;

    public GameEvent stateChangeEvent;

    private void Start()
    {
        states = new Dictionary<string, StateMachine>();
    }

    public List<StateID> GetCurrentState()
    {
        List<StateID> list = new List<StateID>();

        /*
        Debug.Log("Current States:");
        foreach(KeyValuePair<string, StateMachine> pair in states)
        {
            Debug.Log("\t["+pair.Key+"] --- ["+pair.Value.CurrentState+"]");
            list.Add(pair.Value.CurrentState);
        }*/

        foreach(StateMachine state in states.Values)
        {
            list.Add(state.CurrentState);
        }

        return list;
    }

    public StateID GetCurrentStateOf(string layer)
    {
        StateMachine state;
        if (states.TryGetValue(layer, out state))
        {
            return state.CurrentState;
        }
        return StateID.None;
    }

    public bool IsState(StateID id)
    {
        return GetCurrentState().Contains(id);
    }

    public void ChangeState(string layer, StateID id)
    {
        StateMachine state;
        if (states.TryGetValue(layer, out state))
        {
            state.CurrentState = id;
        }
        else
        {
            state = new StateMachine();
            state.CurrentState = id;
            states.Add(layer, state);
        }

        TriggerEvent(layer);
    }

    private void TriggerEvent(string layer)
    {
        StateChangeData data = new StateChangeData();
        data.changedStateLayer = layer;
        data.currentStates = GetCurrentState();
        stateChangeEvent.Trigger(data);
    }
}
