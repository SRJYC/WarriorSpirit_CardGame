using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(DisplaySwitch))]
public class RightClickCloseWindow : MonoBehaviour
    ,IPointerClickHandler
{
    private DisplaySwitch m_Switch;

    void Start()
    {
        m_Switch = GetComponent<DisplaySwitch>();
    }

    public void OnPointerClick(PointerEventData data)
    {
        //right click card
        if (data.button == PointerEventData.InputButton.Right)
        {
            m_Switch.SetActive(false);
        }
    }
}
