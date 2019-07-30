using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverOnTrigger : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public GameEvent m_Event;

    public float m_TimeRequire = 1.0f;

    void Start()
    {
    }

    public void OnPointerEnter(PointerEventData data)
    {
        //Debug.Log("Enter");
        if (m_TimeRequire > 0)
            StartCoroutine(StayHover());
        else
            TriggerEnterEvent();
    }

    public void OnPointerExit(PointerEventData data)
    {
        //Debug.Log("Exit");
        if (m_TimeRequire > 0)
            StopAllCoroutines();

        TriggerExitEvent();
    }

    protected virtual void TriggerEnterEvent()
    {
        m_Event.Trigger();
    }

    protected virtual void TriggerExitEvent()
    {
        m_Event.Trigger();
    }

    protected IEnumerator StayHover()
    {
        yield return new WaitForSeconds(m_TimeRequire);

        TriggerEnterEvent();
    }
}

