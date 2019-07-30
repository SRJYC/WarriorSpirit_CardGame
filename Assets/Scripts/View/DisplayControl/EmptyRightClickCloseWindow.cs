using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyRightClickCloseWindow : MonoBehaviour
{
    public GameEvent m_EmptyLeftClickEvent;
    private DisplaySwitch m_Switch;

    void Start()
    {
        m_Switch = GetComponent<DisplaySwitch>();
    }

    private void OnEnable()
    {
        m_EmptyLeftClickEvent.RegisterListenner(Hide);
    }

    private void OnDisable()
    {
        m_EmptyLeftClickEvent.UnregisterListenner(Hide);
    }

    private void Hide(GameEventData eventData = null)
    {
        m_Switch.SetActive(false);
    }


}
