using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDestroy
{
    public readonly Unit m_Owner;

    public UnitEvent m_DestroyEvent;
    public UnitEvent m_StatsChangeEvent;

    public UnitDestroy(Unit unit, UnitEvent destroy, UnitEvent statsChange)
    {
        m_Owner = unit;

        m_DestroyEvent = destroy;
        m_StatsChangeEvent = statsChange;

        Register();
    }

    public void Destroy()
    {
        RemoveFromOther();

        Unregister();

        ParticleManager.Instance.PlayEffect(ParticleManager.ParticleType.burst, m_Owner.gameObject);

        TriggerEvent();

        GameObject.Destroy(m_Owner.gameObject);
    }

    private void TriggerEvent()
    {
        SingleUnitData data = new SingleUnitData();
        data.m_Unit = m_Owner;

        m_DestroyEvent.Trigger(m_Owner, data);
    }

    private void RemoveFromOther()
    {
        Player player = PlayerManager.Instance.GetPlayer(m_Owner.m_PlayerID);
        player.m_Hand.RemoveFromHand(m_Owner.gameObject);
        player.m_UnitsOnBoard.RemoveFromBoard(m_Owner);

        m_Owner.m_Position.RemoveFromBoard();
        OrderManager.Instance.RemoveUnit(m_Owner);
    }

    private void Register()
    {
        m_StatsChangeEvent.RegisterListenner(m_Owner, CheckDUR);
    }

    private void Unregister()
    {
        m_StatsChangeEvent.UnregisterListenner(m_Owner, CheckDUR);
    }

    private void CheckDUR(GameEventData eventData)
    {
        UnitStatsChangeData data = eventData.CastDataType<UnitStatsChangeData>();
        if (data == null)
            return;

        if(data.m_ChangedStats == UnitStatsProperty.DUR)
        {
            bool negative = m_Owner.m_Data.GetStat(UnitStatsProperty.Negative) > 0;

            if (negative && data.m_AfterChange >= 0)
                Destroy();
            else if (!negative && data.m_AfterChange <= 0)
                Destroy();
        }
    }
}
