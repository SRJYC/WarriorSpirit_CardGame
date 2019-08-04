using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ActionInfoDisplay : MonoBehaviour
{
    public TextMeshProUGUI m_Text;

    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display(string text)
    {
        m_Text.text = text;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
