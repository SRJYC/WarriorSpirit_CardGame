using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RankInfoSceneManager : MonoBehaviour
{
    public AllSpiritCollection m_AllSpirits;

    public UnitData m_SelectedSpirit;

    [Header("Selection Display")]
    public CardDisplay m_SelectionDisplay;
    public DisplayList m_BaseSpiritList;
    public DisplayList m_HighRankSpiritList;

    public DisplayCondition displayCondition;

    [Header("Changing Event")]
    public GameEvent m_ChangeConditionEvent;
    public GameEvent m_ChangeSelectEvent;

    public bool firstDisplay = true; 
    private void Start()
    {
        m_ChangeConditionEvent.RegisterListenner(ChangeCondition);
        m_ChangeSelectEvent.RegisterListenner(ChangeSelect);
    }

    private void Update()
    {
        if (firstDisplay && m_SelectedSpirit != null)
        {
            firstDisplay = false;
            Display();
        }
    }

    private void OnDestroy()
    {
        m_ChangeConditionEvent.UnregisterListenner(ChangeCondition);
        m_ChangeSelectEvent.UnregisterListenner(ChangeSelect);

        CancelInvoke();
    }

    void Display()
    {
        //change center card
        m_SelectionDisplay.Display(m_SelectedSpirit);

        //get all possible base spirits
        List<UnitData> baseSpirits = m_AllSpirits.GetOriginSpirits(m_SelectedSpirit);
        m_BaseSpiritList.Display(baseSpirits.ToArray());

        //get all high rank spirits;
        List<UnitData> highRank = new List<UnitData>();
        GetHighRankSpirits(highRank);
        m_HighRankSpiritList.Display(highRank.ToArray());

        displayCondition.Display(null);
    }

    private void GetHighRankSpirits(List<UnitData> highRank)
    {
        foreach (RankUpCondition condition in m_SelectedSpirit.m_RankUps)
        {
            highRank.Add(condition.m_HighRankSpirit);
        }
    }


    void ChangeSelect(GameEventData eventData)
    {
        CardPreviewData data = eventData.CastDataType<CardPreviewData>();

        m_SelectedSpirit = data.m_UnitData;

        Invoke("Display",0.1f);
    }

    void ChangeCondition(GameEventData eventData)
    {
        CardPreviewData data = eventData.CastDataType<CardPreviewData>();

        UnitData unit = data.m_UnitData;

        RankUpCondition condition = GetCondition(unit, m_SelectedSpirit);

        displayCondition.Display(condition);
    }

    private static RankUpCondition GetCondition(UnitData baseSpirit, UnitData highRankSpirit)
    {
        foreach (RankUpCondition condition in baseSpirit.m_RankUps)
        {
            if (condition.m_HighRankSpirit == highRankSpirit)
                return condition;
        }
        return null;
    }
}
