using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverOnControl : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public GameObject controlledObject;


    void Start()
    {
    }

    public void OnPointerEnter(PointerEventData data)
    {
        controlledObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData data)
    {
        controlledObject.SetActive(false);
    }
}
