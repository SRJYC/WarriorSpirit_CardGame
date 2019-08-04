using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmButton : MonoBehaviour
{
    public delegate void Callback();

    public Callback callback;

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
        gameObject.SetActive(true);
        callback = null;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        callback = null;
    }
}
