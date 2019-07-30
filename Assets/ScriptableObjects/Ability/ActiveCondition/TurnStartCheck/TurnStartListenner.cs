using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityActive/TurnStartListenner")]
public class TurnStartListenner : ActiveListenner
{
    public override void Trigger(GameEventData eventData)
    {
        base.Trigger(eventData);

        m_Owner.Trigger();
    }

}
