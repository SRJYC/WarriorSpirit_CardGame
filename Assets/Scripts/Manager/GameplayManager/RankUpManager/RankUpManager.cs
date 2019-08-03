using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankUpManager : MonoBehaviour
{
    [Header("Trigger")]
    public GameEvent m_RankUpEvent;

    [Header("Selection")]
    public GameEvent m_DisplayCardOptionEvent;
    public GameEvent m_DisplayRankUpRequirementEvent;
    public SelectFromCardOptions m_PlayerSelect;
    public string m_Message;

    private IEnumerator coroutine;
    private RankUpData data;

    [Header("Phase")]
    public GameEvent m_RankUpPhaseEvent;
    public GameEvent m_EndRankUpPhaseEvent;
    private bool busy = false;

    private void Start()
    {
        busy = false;
    }

    public void OnEnable()
    {
        m_RankUpEvent.RegisterListenner(Trigger);
    }

    public void OnDisable()
    {
        m_RankUpEvent.UnregisterListenner(Trigger);
    }

    public void Trigger(GameEventData eventData)
    {
        if (busy)
            return;

        data = eventData.CastDataType<RankUpData>();
        if (data == null)
            return;

        Debug.Log("Receive");

        List<RankUpCondition> conditions = RankUpManagerCheckPhase.GetAllConditions(data.data1,data.data2,data.block);
        
        if (conditions.Count <= 0)
        {
            GameMessage.Instance.Display("No Avaliable Rank Up Option.");
            return;
        }

        Debug.Log("Get Choice");
        //foreach(RankUpCondition rankUpCondition in conditions)
        //{
        //    Debug.Log("\t" + rankUpCondition.name);
        //}

        EnterPhase();

        DisplayOptions(conditions);

        coroutine = GetPlayerChoice();
        StartCoroutine(coroutine);
    }

    private void DisplayOptions(List<RankUpCondition> conditions)
    {
        CardSelectOptionsData data = new CardSelectOptionsData();

        data.m_Switch = true;
        data.isCondition = true;
        data.conditions = conditions.ToArray();

        m_DisplayCardOptionEvent.Trigger(data);
    }

    IEnumerator GetPlayerChoice()
    {
        m_PlayerSelect.m_Num = 1;
        m_PlayerSelect.m_Message = m_Message;
        m_PlayerSelect.GetInfo(Cancel);

        while(!m_PlayerSelect.m_Done)
        {
            yield return new WaitForSeconds(.1f);
        }

        if (m_PlayerSelect.m_selectedCardPreviewList.Count > 0)
        {
            Summon();
        }

        HideWindow();

        ExitPhase();
    }

    private void Summon()
    {
        Debug.Log("Summon High Rank Unit");
        GameObject selectedCard = m_PlayerSelect.m_selectedCardPreviewList[0];

        RankUpManagerSummonPhase.Summon(selectedCard, data.block, data.data1, data.data2);
    }

    public void Cancel()
    {
        StopCoroutine(coroutine);
        HideWindow();
        ExitPhase();
    }

    private void HideWindow()
    {
        RankUpConditionData data = new RankUpConditionData();
        data.m_Switch = false;
        m_DisplayRankUpRequirementEvent.Trigger(data);

        CardSelectOptionsData data2 = new CardSelectOptionsData();
        data2.m_Switch = false;
        m_DisplayCardOptionEvent.Trigger(data2);
    }

    private void EnterPhase()
    {
        busy = true;
        m_RankUpPhaseEvent.Trigger();
    }

    private void ExitPhase()
    {
        busy = false;
        m_EndRankUpPhaseEvent.Trigger();
    }
}
