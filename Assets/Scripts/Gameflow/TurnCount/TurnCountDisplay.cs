using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TurnCountDisplay : MonoBehaviour
{
    public GameEvent turnStartEvent;
    public GameEvent turnCountEvent;

    public TextMeshProUGUI text;
    public float displayTime = 1.0f;

    private int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;

        turnStartEvent.RegisterListenner(Display);

        Hide();
    }

    private void OnDestroy()
    {
        turnStartEvent.UnregisterListenner(Display);
        CancelInvoke();
    }

    void Display(GameEventData data)
    {
        gameObject.SetActive(true);

        count++;
        text.text = "Turn " + count;

        Invoke("Hide", displayTime);

        Trigger();
    }

    void Trigger()
    {
        SingleIntData data = new SingleIntData();
        data.m_Int = count;
        turnCountEvent.Trigger(data);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
