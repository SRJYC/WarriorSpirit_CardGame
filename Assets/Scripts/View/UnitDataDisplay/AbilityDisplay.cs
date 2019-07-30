using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class AbilityDisplay : UnitDataDisplay
{
    public GameObject m_AbilityBlock;

    private Stack<GameObject> m_Actives = new Stack<GameObject>();
    private Stack<GameObject> m_Inactives = new Stack<GameObject>();

    [SerializeField] private int count = 0;
    [SerializeField] private Vector3 originalScale = Vector3.one;
    public override void Display(UnitData data)
    {
        ResetObject();

        List<Ability> abilities = data.m_Abilities;

        InitAbilities(abilities);

        ScaleInfoPanel();
    }

    private void ScaleInfoPanel()
    {
        Vector3 changeOfScale = new Vector3(1, count, 1);
        this.transform.localScale = new Vector3(originalScale.x * changeOfScale.x, originalScale.y * changeOfScale.y, originalScale.z * changeOfScale.z);

        foreach(GameObject block in m_Actives)
        {
            block.transform.localScale = new Vector3(1.0f / changeOfScale.x, 1.0f / changeOfScale.y, 1.0f / changeOfScale.z);
        }
    }

    private void InitAbilities(List<Ability> abilities)
    {
        foreach (Ability ability in abilities)
        {
            if (!ability.m_IsDisplay)
                continue;

            count++;

            GameObject block = GetActive();

            block.SetActive(true);
            m_Actives.Push(block);

            block.transform.SetParent(this.transform);

            block.GetComponent<SingleAbilityDisplay>().Display(ability);
        }
    }

    private GameObject GetActive()
    {
        if (m_Inactives.Count > 0)
            return m_Inactives.Pop();
        else
            return Instantiate(m_AbilityBlock);
    }

    private void OnDisable()
    {
        ResetObject();
    }

    private void ResetObject()
    {
        count = 0;

        this.transform.localScale = originalScale;

        while (m_Actives.Count > 0)
        {
            GameObject property = m_Actives.Pop();
            property.SetActive(false);
            m_Inactives.Push(property);
        }
    }
}
