using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public Player player1;
    public Player player2;

    private List<Player> playerList;

    public bool test;

    public ResetMana resetMana;
    
    public void Init()
    {
        playerList = new List<Player>();

        if(GameData.Instance.playerId == PlayerID.Player1)
        {
            playerList.Add(player1);
            player1.Init(GameData.Instance.playerId);

            playerList.Add(player2);
            player2.Init(GameData.Instance.enemyId);
        }
        else
        {
            playerList.Add(player2);
            player2.Init(GameData.Instance.playerId);

            playerList.Add(player1);
            player1.Init(GameData.Instance.enemyId);
        }

        resetMana.Init(player1.m_Mana, player2.m_Mana);
    }

    public Player GetPlayer(PlayerID id)
    {
        return playerList[(int)id];
    }

    public PlayerID GetLocalPlayerID()
    {
        return player1.m_ID;
    }
}
