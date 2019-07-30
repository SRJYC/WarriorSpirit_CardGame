using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrderMarkDisplay : MonoBehaviour
    ,IPointerEnterHandler
    ,IPointerExitHandler
{
    [Header("Background")]
    public Image background;
    public Color inactiveColor;
    public Color allyColor;
    public Color enemyColor;
    private Color activeColor;

    [Header("Symbol")]
    public Image symbolImage;
    public Sprite center;
    public Sprite front;
    public Sprite back;

    [Header("Speed Label")]
    public TextMeshProUGUI text;
    public string label = "SPD: ";

    private Unit m_Unit;
    private HighlightType highlightType = HighlightType.CardIndicate;
    private LayoutGroup group;

    private Vector3 origin;

    public void SetActiveColor(bool active)
    {
        if (activeColor == null)
            active = false;

        Color c = active ? activeColor : inactiveColor;
        background.color = c;
    }

    public void Display(Unit unit, bool ally)
    {
        m_Unit = unit;

        ChangeBackgroundColor(ally);

        ChangeSymbol(unit);

        ChangeLabel(unit);
    }

    private void ChangeLabel(Unit unit)
    {
        int speed = unit.m_Data.GetStat(UnitStatsProperty.SPD);
        text.text = label + speed;
    }

    private void ChangeBackgroundColor(bool ally)
    {
        activeColor = ally ? allyColor : enemyColor;
        background.color = activeColor;
    }

    private void ChangeSymbol(Unit unit)
    {
        switch (unit.m_Position.m_Block.m_RowType)
        {
            case FieldBlockType.Front:
                symbolImage.sprite = front;
                break;
            case FieldBlockType.Center:
                symbolImage.sprite = center;
                break;
            case FieldBlockType.Back:
                symbolImage.sprite = back;
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(m_Unit != null)
            HighlightManager.Instance.Highlight(m_Unit.gameObject, highlightType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_Unit != null)
            HighlightManager.Instance.Unhighlight(m_Unit.gameObject, highlightType);
    }

    [ContextMenu("Enlarge")]
    public void Enlarge()
    {
        //Debug.Log("Enlarge");
        origin = this.transform.localScale;
        this.transform.localScale = new Vector3(1.5f * origin.x, 1.5f * origin.y, 1 * origin.z);

        ResetParentLayoutGroup();

        //change focus
        ScrollRect scrollRect = GetComponentInParent<ScrollRect>();
        float scrollValue = 1 + GetComponent<RectTransform>().anchoredPosition.y / scrollRect.content.rect.height;
        scrollRect.verticalNormalizedPosition = scrollValue;
    }

    [ContextMenu("Resize")]
    public void Resize()
    {
        this.transform.localScale = origin;

        ResetParentLayoutGroup();
    }

    private void ResetParentLayoutGroup()
    {
        if (group == null)
            group = this.transform.parent.GetComponent<LayoutGroup>();

        if(group != null)
        {
            group.enabled = false;
            group.enabled = true;
        }
    }

}
