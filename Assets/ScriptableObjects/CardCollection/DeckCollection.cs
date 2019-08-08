using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIDecks")]
public class DeckCollection : ScriptableObject
{
    public List<CardCollection> decks;

    public CardCollection GetDeck()
    {
        return decks[Random.Range(0, decks.Count)];
    }
}
