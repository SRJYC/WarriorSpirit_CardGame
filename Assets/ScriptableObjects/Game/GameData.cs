using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Other/GameData")]
public class GameData : SingletonScriptableObject<GameData>
{
    public PlayerID playerId;
    public PlayerID enemyId { get { return playerId == PlayerID.Player1 ? PlayerID.Player2 : PlayerID.Player1; } }

    public List<CardCollection> decks;

    public CardCollection GetDeck(PlayerID id)
    {
        return Instantiate(decks[(int)id]);
    }
}
