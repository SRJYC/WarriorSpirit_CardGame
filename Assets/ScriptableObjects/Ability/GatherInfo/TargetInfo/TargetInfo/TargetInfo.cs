using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityInfo/Target")]
public class TargetInfo : AbilityInfo
{
    [HideInInspector] public List<Unit> m_Targets;
    [HideInInspector] public List<FieldBlock> m_Blocks;

    public bool highlightCard = true;
    public bool highlightBlock = true;
    public HighlightType cardHighlightType;
    public HighlightType blockHighlightType;

    public void Reset()
    {
        m_Blocks = new List<FieldBlock>();
        m_Targets = new List<Unit>();
    }

    public override void Highlight()
    {
        if(highlightBlock)
        {
            foreach (FieldBlock block in m_Blocks)
            {
                HighlightManager.Instance.Highlight(block.gameObject, blockHighlightType);
            }
        }

        if(highlightCard)
        {
            foreach (Unit unit in m_Targets)
            {
                HighlightManager.Instance.Highlight(unit.gameObject, cardHighlightType);
            }
        }
    }

    public override void Unhighlight()
    {
        if(highlightBlock)
        {
            foreach (FieldBlock block in m_Blocks)
            {
                HighlightManager.Instance.Unhighlight(block.gameObject, blockHighlightType);
            }
        }

        if(highlightCard)
        {
            foreach (Unit unit in m_Targets)
            {
                HighlightManager.Instance.Unhighlight(unit.gameObject, cardHighlightType);
            }
        }
    }
}
