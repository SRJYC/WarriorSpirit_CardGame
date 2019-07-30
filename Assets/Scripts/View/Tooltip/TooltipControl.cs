using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipControl : MonoBehaviour
{
    public RectTransform m_ContentTransform;

    [SerializeField]private Vector2 m_Offset = Vector2.zero;

    [SerializeField] private LayoutGroup m_LayoutGroup = null;
    private bool refresh;
    private void OnEnable()
    {
        refresh = true;
    }

    // Update is called once per frame
    void Update()
    {
        Refresh();

        Vector2 offset = m_Offset;
        int region = MouseScreenCheck.CheckCursorQuadrant();
        switch (region)
        {
            case 2:
                offset.x = -offset.x;
                break;
            case 3:
                offset.y = -offset.y;
                break;
            case 4:
                offset.x = -offset.x;
                offset.y = -offset.y;
                break;
        }

        Vector2 mousePos = Input.mousePosition;
        this.transform.position = mousePos + offset;
    }

    private void Refresh()
    {
        if (refresh)
        {
            m_LayoutGroup.enabled = false;
            m_LayoutGroup.enabled = true;

            float x = m_ContentTransform.rect.width / 2;
            float y = m_ContentTransform.rect.height / 2;
            m_Offset = new Vector2(x, y);

            refresh = false;
        }
    }

}