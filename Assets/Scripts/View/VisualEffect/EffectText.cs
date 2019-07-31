using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class EffectText : MonoBehaviour
{
    public TextMeshProUGUI m_Text;

    public PlayableDirector m_Playable;

    public void Display(Vector3 pos, string msg, Color color)
    {
        this.transform.position = pos;

        m_Text.text = msg;

        m_Text.color = color;

        gameObject.SetActive(true);

        m_Playable.Play();
    }
}
