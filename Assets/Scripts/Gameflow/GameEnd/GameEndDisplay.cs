using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameEndDisplay : MonoBehaviour
{
    public GameEvent gameEndEvent;

    public TextProperty winText;
    public TextProperty loseText;
    public TextProperty endText;

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        gameEndEvent.RegisterListenner(Display);

        Hide();
    }

    private void OnDestroy()
    {
        gameEndEvent.UnregisterListenner(Display);
    }

    void Display(GameEventData data)
    {
        if(data == null)
        {
            gameObject.SetActive(true);
            text.text = endText.ToString();
        }
        else
        {
            SingleUnitData unitData = data.CastDataType<SingleUnitData>();
            if (unitData == null)
                return;

            PlayerID loseId = unitData.m_Unit.m_PlayerID;

            TextProperty word = loseId == PlayerManager.Instance.GetLocalPlayerID() ? loseText : winText;

            gameObject.SetActive(true);
            text.text = word.ToString();
        }
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
