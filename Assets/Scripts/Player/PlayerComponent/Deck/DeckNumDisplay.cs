using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeckNumDisplay : MonoBehaviour
{
    public Deck deck;
    public TextMeshProUGUI text;

    private int num;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (num != deck.CardCount)
        {
            num = deck.CardCount;
            text.text = num.ToString();
        }
    }
}

