using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankUpConditionDisplay : MonoBehaviour
{
    public GameEvent m_Event;

    public TextMeshProUGUI m_condition1Text;
    public TextMeshProUGUI m_condition2Text;

    public void Start()
    {
        gameObject.SetActive(false);
    }
    private void Awake()
    {
        Register();
    }

    private void OnDestroy()
    {
        Unregister();
    }

    public void Register()
    {
        m_Event.RegisterListenner(Show);
    }

    public void Unregister()
    {
        m_Event.UnregisterListenner(Show);
    }

    public void Show(GameEventData eventData)
    {
        RankUpConditionData data = eventData.CastDataType<RankUpConditionData>();
        if (data == null)
            return;

        if (data.m_Switch)
        {
            gameObject.SetActive(true);

            //Debug.Log("Condtion Receive ["+data.m_Condition+"]");
            //Debug.Log("String 1 : [" + data.m_Condition.GetStringOfCondition1() + "]");
            //Debug.Log("String 2 : [" + data.m_Condition.GetStringOfCondition2() + "]");
            m_condition1Text.text = data.m_Condition.GetStringOfCondition1();
            m_condition2Text.text = data.m_Condition.GetStringOfCondition2();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
