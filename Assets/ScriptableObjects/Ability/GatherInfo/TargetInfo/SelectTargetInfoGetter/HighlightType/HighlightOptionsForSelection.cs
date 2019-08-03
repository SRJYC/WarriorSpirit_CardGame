using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Other/HighlightOption")]
public class HighlightOptionsForSelection : ScriptableObject
{
    public HighlightType m_avaliableCardType;
    public HighlightType m_avaliableBlockType;
    public HighlightType m_selectedCardType;
    public HighlightType m_selectedBlockType;
}
