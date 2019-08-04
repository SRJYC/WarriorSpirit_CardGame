using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateID
{
    None,
    GameStart,
    TurnStartPhase,
    DrawCardPhase,
    PlayCardPhase,
    RankUpPhase,
    OrderInitPhase,
    UnitTurnStartPhase,
    PlayerTurn,
    EnemyTurn,
    ActionPhase,
    UnitTurnEndPhase,
    TurnEndPhase,
    GameEnd,
    Gameplay,
}