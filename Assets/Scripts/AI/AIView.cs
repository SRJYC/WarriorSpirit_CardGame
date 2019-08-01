using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPlayer
{
    public class AIView : Singleton<AIView>
    {
        public Player AI;
        public Player player;

        [HideInInspector] public Unit AllyWarrior;
        [HideInInspector] public Unit EnemyWarrior;
        public List<Unit> AllAllyUnits { get { return AI.m_UnitsOnBoard.m_Units; } }
        [HideInInspector] public List<Unit> AllAllySpirits;

        public List<Unit> AllEnemyUnits { get { return player.m_UnitsOnBoard.m_Units; } }
        [HideInInspector] public List<Unit> AllEnemySpirits;

        [HideInInspector] public List<Unit> CardsInHand;

        [HideInInspector] public FieldBlock EmptyFront;
        [HideInInspector] public FieldBlock EmptyBack;

        [HideInInspector] public int Mana;
        public void Start()
        {

        }

        public void View()
        {
            GetWarrior(AllAllyUnits, ref AllyWarrior);

            GetSpirits(ref AllAllySpirits, AllAllyUnits, AllyWarrior);

            GetPlayer();

            GetWarrior(AllEnemyUnits, ref EnemyWarrior);

            GetSpirits(ref AllEnemySpirits, AllEnemyUnits, EnemyWarrior);


            CardsInHand = new List<Unit>();
            foreach (GameObject go in AI.m_Hand.cards)
            {
                CardsInHand.Add(go.GetComponent<Unit>());
            }


            FieldController field = BoardManager.Instance.GetFieldController(AI.m_ID);
            GetEmptyBlock(field, FieldBlockType.Front, ref EmptyFront);
            GetEmptyBlock(field, FieldBlockType.Back, ref EmptyBack);

            Mana = AI.m_Mana.currentMana;
        }

        private void GetEmptyBlock(FieldController field, FieldBlockType line, ref FieldBlock block)
        {
            List<FieldBlock> blocks = field.GetBlocksOfLine(line);
            foreach (FieldBlock b in blocks)
            {
                if (b.m_Unit == null)
                {
                    block = b;
                    return;
                }
            }
            block = null;
        }

        private void GetWarrior(List<Unit> all, ref Unit warrior)
        {
            if (warrior == null)
            {
                foreach (Unit unit in all)
                {
                    if (unit.m_Data.IsWarrior)
                    {
                        warrior = unit;
                        return;
                    }
                }
            }
        }

        private void GetPlayer()
        {
            if (player == null)
            {
                PlayerID id = AI.m_ID == PlayerID.Player1 ? PlayerID.Player2 : PlayerID.Player1;
                player = PlayerManager.Instance.GetPlayer(id);
            }
        }

        private void GetSpirits(ref List<Unit> spirits, List<Unit> all, Unit warrior)
        {
            spirits = new List<Unit>();
            spirits.AddRange(all);
            spirits.Remove(warrior);
        }
    }
}
