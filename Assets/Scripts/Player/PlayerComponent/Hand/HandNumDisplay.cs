using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HandNumDisplay : MonoBehaviour
{
    public Hand hand;
    public TextMeshProUGUI text;

    private int num;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (num != hand.CardCount)
        {
            num = hand.CardCount;
            text.text = num.ToString();
        }
    }
}
