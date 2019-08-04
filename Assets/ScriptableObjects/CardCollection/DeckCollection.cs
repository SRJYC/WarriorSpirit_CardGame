using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Deck")]
public class DeckCollection : ScriptableObject
{
    public List<CardCollection> decks;
}
