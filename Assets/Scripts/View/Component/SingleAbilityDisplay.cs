using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SingleAbilityDisplay : MonoBehaviour
{
    public Ability m_Ability;

    public TextMeshProUGUI m_NameText;
    public TextMeshProUGUI m_DescriptionText;
    public Image m_Background;

    [Header("Difference between Action and Passive")]
    [SerializeField] private Color m_ActionColor = Color.white;
    [SerializeField] private Sprite m_ActionBackground = null;
    [SerializeField] private Color m_PassiveColor = Color.white;
    [SerializeField] private Sprite m_PassiveBackground = null;

    [SerializeField] private TMP_ColorGradient m_InactiveColor = null;
    public void Display(Ability data)
    {
        m_Ability = data;
        DisplayText(data.m_Data.m_AbilityName, data.m_Data.m_Description);
        ChangeForAction(data.m_IsAction);
    }

    private void DisplayText(string name, string description)
    {
        m_NameText.text = name;
        m_DescriptionText.text = description;
    }

    private void ChangeForAction(bool isAction)
    {
        if (isAction)
        {
            m_Background.sprite = m_ActionBackground;
            m_NameText.color = m_ActionColor;
        }
        else
        {
            m_Background.sprite = m_PassiveBackground;
            m_NameText.color = m_PassiveColor;
        }
    }

    private void DisplayActive(bool isActive)
    {
        if(isActive)
        {
            m_NameText.enableVertexGradient = false;
            m_DescriptionText.enableVertexGradient = false;
        }
        else
        {
            m_NameText.enableVertexGradient = true;
            m_DescriptionText.enableVertexGradient = true;

            m_NameText.colorGradientPreset = m_InactiveColor;
            m_DescriptionText.colorGradientPreset = m_InactiveColor;
        }
    }
}
