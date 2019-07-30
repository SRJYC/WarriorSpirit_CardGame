using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject container;

    public int limit;
    public List<GameObject> cards = new List<GameObject>();

    public int CardCount { get { return cards.Count; } }
    public bool ReachMax { get { return cards.Count >= limit; } }
    public void AddCard(GameObject card)
    {
        if(container != null)
            card.transform.SetParent(container.transform);

        cards.Add(card);
    }

    public void RemoveFromHand(GameObject card)
    {
        cards.Remove(card);
    }
}
