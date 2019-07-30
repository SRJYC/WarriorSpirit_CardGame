using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SingleAbilityDisplay))]
public class ClickTriggerAbility : MonoBehaviour
    ,IPointerClickHandler
{
    private SingleAbilityDisplay m_Display;

    private Unit unit;
    private Ability ability;

    void Start()
    {
        m_Display = GetComponent<SingleAbilityDisplay>();
    }

    void OnDisable()
    {
        unit = null;
        ability = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //only active for left click
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        CheckAbility();

        if (ability.m_Owner == null)
            return;

        if (!ability.m_IsActive)
            return;

        ActionManager.Instance.TriggerAction(ability);
    }

    private void CheckAbility()
    {
        if (unit == null || ability == null)
        {
            ability = m_Display.m_Ability;
            unit = ability.m_Owner;
        }
    }
}
