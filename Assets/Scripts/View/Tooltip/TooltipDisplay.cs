using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text = null;
    [SerializeField] private GameEvent m_Event = null;

    [SerializeField]private float m_DisplayTime = 0.0f;
    private bool m_active = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Event.RegisterListenner(Trigger);
        gameObject.SetActive(m_active);
    }

    void OnDestroy()
    {
        m_Event.UnregisterListenner(Trigger);

        CancelInvoke();
    }

    public void Display(string message)
    {
        m_Text.text = message;

        if (m_DisplayTime > 0)
        {
            CancelInvoke();
            Invoke("Disappear", m_DisplayTime);
        }
    }

    public void Trigger(GameEventData eventData)
    {
        TooltipData data = eventData.CastDataType<TooltipData>();
        if (data == null)
            return;

        if (data.m_Switch && !m_active)
            Display(data.m_Message);

        SetActive(data.m_Switch);
    }

    public void SetActive(bool active)
    {
        if (m_active == active)
            return;

        m_active = active;
        gameObject.SetActive(m_active);
    }

    private void Disappear()
    {
        SetActive(false);
    }


    [ContextMenu("Test")]
    public void Test()
    {
        int n = Random.Range(1, 100);

        string s = "";
        for(int i=0; i<n; i++)
        {
            char a = (char)Random.Range(49, 123);
            s += a;
        }
        Display(s);
    }
}
