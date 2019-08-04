using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class GameMessage : Singleton<GameMessage>
{
    public TextMeshProUGUI m_Text;
    public float m_DisplayTime;
    public CanvasGroup m_CanvasGroup;
    public PlayableDirector playableDirectior;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    public void Display(string msg)
    {
        CancelInvoke();
        m_CanvasGroup.alpha = 1;

        m_Text.text = msg;

        gameObject.SetActive(true);

        Invoke("Hide", m_DisplayTime);
    }

    private void Hide()
    {
        playableDirectior.Play();
    }
}
