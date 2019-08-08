using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : UnitDataDisplay
{
    public UnitData m_CardData;

    [Header("UI")]
    [SerializeField] private Image m_Background = null;
    [SerializeField] private TextMeshProUGUI m_NameText = null;
    [SerializeField] private Image m_Art = null;
    [SerializeField] private GameObject m_Icon = null;

    public override void Display(UnitData data)
    {
        m_CardData = data;
        Refresh();
    }

    public void OnEnable()
    {
        Refresh();
    }

    public void OnValidate()
    {
        Refresh();
    }

    private void Refresh()
    {
        if (m_CardData == null)
            return;

        m_Background.color = RankColor.Instance.GetColor(m_CardData.Rank);

        m_NameText.SetText(m_CardData.UnitName);
        m_Art.sprite = m_CardData.Artwork;
        m_Icon.SetActive(m_CardData.IsUnique);
    }
}
