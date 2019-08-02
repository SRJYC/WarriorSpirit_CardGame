using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : Singleton<ActionManager>
{
    public bool m_IsBusy = false;
    public GameEvent m_ActionPhaseEvent;
    public GameEvent m_EndActionPhaseEvent;
    public GameEvent m_TurnPassEvent;


    private Ability m_Action;

    [HideInInspector] public ConfirmButton m_ConfirmButton;
    [HideInInspector] public ActionInfoDisplay m_ActionMessage;

    private bool click = false;
    private bool confirm = false;

    private void Awake()
    {
        m_ConfirmButton = FindObjectOfType<ConfirmButton>();
        m_ActionMessage = FindObjectOfType<ActionInfoDisplay>();
    }
    void Start()
    {
        m_IsBusy = false;
    }

    public void TriggerAction(Ability action, bool Check = true, bool enterActionPhase = true)
    {
        Debug.Log("Trigger Ability [" + action.m_Data.m_AbilityName + "]");
        if (m_IsBusy)
            return;

        Debug.Log("Not Busy");
        if (Check && !CheckAvaliablity(action))
            return;

        Debug.Log("pass check");
        if (enterActionPhase)
            EnterPhase();

        WindowManager.Instance.FocusBoard();

        Debug.Log("Action Trigger");
        m_Action = action;
        action.Trigger();
    }

    private static bool CheckAvaliablity(Ability action)
    {
        Unit unit = action.m_Owner;
        if (unit == null)
        {
            Debug.Log("Not a real unit");
            return false;
        }
        else if (unit.m_Position.m_Block == null)
        {
            GameMessage.Instance.Display("Spirit is not on Battlefield");
            return false;
        }
        else if (!unit.PlayerControl())
        {
            GameMessage.Instance.Display("Spirit can't be controlled. It's either not its turn or it's enemy.");
            return false;
        }
        else if (action.m_CurrentCD > 0)
        {
            GameMessage.Instance.Display("This Action is still in Cooldown. You may Pass turn instead.");
            return false;
        }
        else if (action.m_ManaCost > 0)
        {
            Player player = PlayerManager.Instance.GetPlayer(unit.m_PlayerID);
            if (action.m_ManaCost > player.m_Mana.currentMana)
            {
                GameMessage.Instance.Display("This Action will cost Mana, make sure you have enough of it.");
                return false;
            }
        }
        return true;
    }

    public void ActionConfirm()
    {
        Debug.Log("Need Confirm");
        if (StateMachineManager.Instance.IsState(StateID.ActionPhase) || StateMachineManager.Instance.IsState(StateID.PlayerTurn))
            StartCoroutine(PlayerConfirm());
        else
            Enemy.Instance.Confirm(TakeEffect);
    }

    IEnumerator PlayerConfirm()
    {
        Debug.Log("Player Confirm");
        click = false;
        confirm = false;

        ShowInfo();

        while (!click)
        {
            yield return null;

            if (!click)//right click anywhere
            {
                click = Input.GetMouseButton(1);
            }
        }

        Debug.Log("Confirm "+ confirm);

        HideInfo();

        if (confirm)
        {
            TakeEffect();
        }

        ExitPhase();
    }

    private void TakeEffect()
    {
        m_Action.TakeEffect();
        Invoke("EndTurn", 1);
    }

    private void ShowInfo()
    {
        m_ActionMessage.Display("Please [Confirm] Your Choice");

        m_ConfirmButton.Show();
        m_ConfirmButton.callback += Confirm;

        m_Action.HighlightInfo();
    }

    private void HideInfo()
    {
        m_ActionMessage.Hide();

        m_ConfirmButton.Hide();

        m_Action.UnhighlightInfo();
    }

    private void Confirm()
    {
        click = true;
        confirm = true;
    }

    public void CancelAction()
    {
        if (!m_IsBusy)
            return;

        m_Action.CancelInfoGetter();
        ExitPhase();
    }

    private void EnterPhase()
    {
        Debug.Log("Enter Phase");
        m_IsBusy = true;
        m_ActionPhaseEvent.Trigger();
    }

    private void ExitPhase()
    {
        Debug.Log("Exit Phase");
        m_IsBusy = false;
        m_EndActionPhaseEvent.Trigger();
    }

    private void EndTurn()
    {
        Debug.Log("End Turn");
        m_TurnPassEvent.Trigger();
    }
}
