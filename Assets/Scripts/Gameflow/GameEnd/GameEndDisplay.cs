using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameEndDisplay : MonoBehaviour
{
    public GameEvent gameEndEvent;

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        gameEndEvent.RegisterListenner(Display);

        Hide();
    }

    private void OnDestroy()
    {
        gameEndEvent.UnregisterListenner(Display);
    }

    void Display(GameEventData data)
    {
        gameObject.SetActive(true);

        text.text = "Game End";
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
