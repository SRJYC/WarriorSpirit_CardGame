using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class GameStartCheck : MonoBehaviour
    {
        public int m_SceneIndex;
        public GameData m_GameData;

        [Header("Player Deck")]
        public DeckDisplay m_DeckDisplay;

        [Header("Enemy Deck")]
        public DeckCollection m_EnemyDecks;

        [Header("Message")]
        public TextProperty m_Text;
        public void GameStart()
        {
            if(m_DeckDisplay.Warning.activeSelf)
            {
                GameMessage.Instance.Display(m_Text.ToString());
            }
            else
            {
                m_GameData.playerId = PlayerID.Player1;
                m_GameData.decks[0] = m_DeckDisplay.deck;
                m_GameData.decks[1] = m_EnemyDecks.GetDeck();

                UnityEngine.SceneManagement.SceneManager.LoadScene(m_SceneIndex);
            }
        }
    }
}
