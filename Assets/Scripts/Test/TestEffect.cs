using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestEffect : MonoBehaviour
    , IPointerClickHandler
{
    public Effect effect;
    public SourceInfo source;
    public TargetInfo target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Unit u = GetComponent<Unit>();
        if (u.m_Position.m_Block == null)
            return;
        source = Instantiate(source);
        target = Instantiate(target);
        source.m_Source = u;
        target.m_Targets = new List<Unit>();
        target.m_Targets.Add(u);
        AbilityInfo[] abilityInfos = new AbilityInfo[] { source,target};
        effect.TakeEffect(abilityInfos);
    }
}
