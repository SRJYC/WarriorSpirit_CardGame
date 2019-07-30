using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityActive/ClickAction")]
public class ClickActionListenner : ActiveListenner
{
    public override void Trigger(GameEventData eventData)
    {
        base.Trigger(eventData);

        SingleAbilityData data = eventData.CastDataType<SingleAbilityData>();

        //Debug.Log(data.m_Ability + " is clicked and owner is "+ m_Owner);
        if (data.m_Ability == m_Owner)
            m_Owner.Trigger();
    }
}
