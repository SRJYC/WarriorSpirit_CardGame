using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPlayer
{
    public class AIManager : Singleton<AIManager>
    {
        public GameEvent m_PassEvent;

        public bool pass = false;
        public UnitData DrawCard(List<UnitData> options)
        {
            AIView.Instance.View();
            return AIMove.DrawCard.Think(options);
        }

        public void PlayCard()
        {
            pass = false;
            StopAllCoroutines();
            StartCoroutine(PlayCardCoroutine());
        }

        public void UseAction()
        {
            //Debug.Log("Use Action");

            AIView.Instance.View();

            Unit unit = OrderManager.Instance.CurrentUnit;
            if (unit.m_PlayerID != AIView.Instance.AI.m_ID)
                return;

            AIMove.UseAction.Think(unit);
        }

        public void Stop()
        {
            StopAllCoroutines();
        }

        IEnumerator PlayCardCoroutine()
        {
            while(!pass)
            {
                AIView.Instance.View();
                AIMove.PlayCard.Think();

                yield return new WaitForSeconds(0.5f);
            }
        }

        public void Pass()
        {
            if (StateMachineManager.Instance.IsState(StateID.EnemyTurn))
                m_PassEvent.Trigger();
            else
                pass = true;
        }
        public List<UnitData> GetCardChoice(List<UnitData> options, int num = 1, bool repeat = false)
        {
            Debug.Log("AI choose random card from options");

            int size = options.Count;
            if (size <= num)
            {
                return options;
            }

            List<UnitData> list = new List<UnitData>();

            List<int> indices = new List<int>();
            for (int i = 0; i < num; i++)
            {
                int index = Random.Range(0, size);
                while (!repeat && indices.Contains(index))
                {
                    index = Random.Range(0, size);
                }
                indices.Add(index);
            }

            foreach (int i in indices)
            {
                list.Add(options[i]);
            }

            return list;
        }

    }
}

