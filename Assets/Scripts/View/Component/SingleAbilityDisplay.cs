﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SingleAbilityDisplay : MonoBehaviour
{
    public Ability m_Ability;

    public TextMeshProUGUI m_NameText;
    public TextMeshProUGUI m_DescriptionText;
    public TextMeshProUGUI m_AdditionalText;
    public Image m_Background;

    [Header("Difference between Action and Passive")]
    [SerializeField] private Color m_ActionColor = Color.white;
    [SerializeField] private Sprite m_ActionBackground = null;
    [SerializeField] private Color m_PassiveColor = Color.white;
    [SerializeField] private Sprite m_PassiveBackground = null;

    [SerializeField] private TMP_ColorGradient m_InactiveColor = null;

    public const string cdText = "CD(current CD):";
    public const string manaText = "Mana Cost:";
    public void Display(Ability data)
    {
        m_Ability = data;

        DisplayCDAndManaCost();
        DisplayText(data.m_AbilityName.ToString(), data.m_Description.ToString());

        ChangeForAction(data.m_IsAction);
    }

    private void DisplayCDAndManaCost()
    {
        string result = "";

        if (m_Ability.m_CD > 0)
            result += cdText + m_Ability.m_CD + "(" + m_Ability.m_CurrentCD + ")";
        if(m_Ability.m_ManaCost > 0)
            result += manaText + m_Ability.m_ManaCost;

        m_AdditionalText.text = result;
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
