using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmButton : MonoBehaviour
{
    public delegate void Callback();
    public Callback callback;

    public TMPro.TextMeshProUGUI m_Label;
    public TextProperty m_LabelText;

    private void Start()
    {
        Hide();
    }

    public void OnClick()
    {
        callback?.Invoke();
    }

    public void Show()
    {
        m_Label.text = m_LabelText.ToString();

        gameObject.SetActive(true);
        callback = null;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        callback = null;
    }
}
