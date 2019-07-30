using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Other/GameEventGroup")]
public class GameEventGroup : ScriptableObject
{
    public List<GameEvent> events;

    public void SetActive(bool set)
    {
        foreach(GameEvent gameEvent in events)
        {
            gameEvent.SetActive(set);
        }
    }
}
