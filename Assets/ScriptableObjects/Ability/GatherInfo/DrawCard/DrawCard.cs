using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityInfoGetter/DrawCard")]
public class DrawCard : AbilityInfoGetter
{
    [Header("Draw Options")]
    public int m_Options = 3;
    public int m_Choice = 1;
    public int m_Times = 1;

    [Header("Card Choice Need To Satisfy")]
    public SingleUnitCondition m_Condition;

    public override void GetInfo(Unit source)
    {
        m_Done = false;

        PlayerID player = source.m_PlayerID;

        PlayerManager.Instance.GetPlayer(player).m_Deck.DrawCard(Done, m_Condition, m_Options, m_Choice, m_Times);
    }

    public override void StoreInfo(AbilityInfo info)
    {
    }

    private void Done(PlayerID id)
    {
        m_Done = true;
    }
}
