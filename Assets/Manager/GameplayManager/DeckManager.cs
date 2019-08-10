using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    [Header("Card")]
    public GameObject cardPrefab;

    [Header("Draw Card Event")]
    public GameEvent m_DrawCard;
    public GameEvent m_EndDrawCard;

    //[Header("Card Select")]
    //public SelectFromCardOptions m_PlayerSelect;
    //public GameEvent m_DisplayCardOptionEvent;

    //private DeckDrawCard m_DrawCardComponent;

    private ObjectPool m_Pool;

    private bool[] draw = new bool[2];
    private void Start()
    {
        m_Pool = new ObjectPool(cardPrefab);

        if (m_DrawCard != null)
            m_DrawCard.RegisterListenner(DrawCardPhaseStart);
    }

    private void OnDestroy()
    {
        if (m_DrawCard != null)
            m_DrawCard.UnregisterListenner(DrawCardPhaseStart);
    }

    public GameObject GetEmptyCard()
    {
        return m_Pool.Get();
    }

    public GameObject GetCard(UnitData data, PlayerID id)
    {
        GameObject card = GetEmptyCard();

        card.GetComponent<Unit>().Init(data, id);

        return card;
    }

    private void DrawCardPhaseStart(GameEventData data)
    {
        //Debug.Log(this + " Reset Notify");
        for (int i=0; i<draw.Length; i++)
        {
            draw[i] = false;
        }
    }

    public void EndDrawCard(PlayerID id)
    {
        //Debug.Log(this + " Receve Notify " + id);

        draw[(int)id] = true;

        for (int i = 0; i < draw.Length; i++)
        {
            if (!draw[i])
            {
                //Debug.Log(i + " isn't true");
                return;
            }
        }

        //Debug.Log(this + " End "+m_EndDrawCard);

        m_EndDrawCard.Trigger();
    }
}
