using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AIPlayer
{
    namespace AIMove
    {
        public class UseAction
        {
            public static void Think(Unit unit)
            {
                List<Ability> abilities = unit.m_Data.m_Abilities;

                List<ActionOption> options = EvaluateAction(abilities);

                if (options.Count == 0)
                {
                    AIAction.Pass.Do();
                    return;
                }

                ActionOption op = Common.RandomOptionOfBest<ActionOption>(options);

                TriggerAction(unit, op);
            }

            private static void TriggerAction(Unit unit, ActionOption op)
            {
                if (CheckTarget(op.ability, unit))
                    AIAction.UseAction.Do(op);
                else
                    AIAction.Pass.Do();
            }

            private static List<ActionOption> EvaluateAction(List<Ability> abilities)
            {
                List<ActionOption> list = new List<ActionOption>();

                foreach (Ability ability in abilities)
                {
                    if (!ability.m_IsAction)
                        continue;

                    if (ability.m_CurrentCD > 0)
                        continue;

                    if (ability.m_ManaCost > AIView.Instance.Mana)
                        continue;

                    ActionOption option = new ActionOption(ability);

                    option.score = AIRule.EvaluateAbility.Evaluate(ability);

                    list.Add(option);
                }

                return list;
            }

            private static bool CheckTarget(Ability ability, Unit unit)
            {
                foreach(AbilityInfoGetter infoGetter in ability.m_InfoGetters)
                {
                    if (infoGetter.m_Automatic)
                        continue;

                    if(infoGetter.GetType() == typeof(SelectTargetInfoGetter))
                    {
                        SelectTargetInfoGetter select = (SelectTargetInfoGetter)infoGetter;
                        select.GetAvaliableTargets(unit);

                        List<FieldBlock> options = select.m_AvaliableBlocks;
                        List<TargetOption> optionsList = EvaluateOptions(ability, options);

                        if (optionsList.Count == 0)
                            return false;

                        SelectTargets(select, optionsList);
                    }
                }
                return true;
            }

            private static void SelectTargets(SelectTargetInfoGetter select, List<TargetOption> optionsList)
            {
                select.Reset();
                select.EnemySelect = true;
                for (int i = 0; i < select.m_Num; i++)
                {
                    TargetOption target = Common.RandomOptionOfBest<TargetOption>(optionsList);
                    select.m_Blocks.Add(target.block);
                    if (!select.m_CanRepeat)
                        optionsList.Remove(target);
                }
            }

            private static List<TargetOption> EvaluateOptions(Ability ability, List<FieldBlock> options)
            {
                List<TargetOption> optionsList = new List<TargetOption>();
                foreach (FieldBlock block in options)
                {
                    TargetOption op = new TargetOption(block);
                    op.score = AIRule.EvaluateTarget.Evaluate(block, ability);
                    optionsList.Add(op);
                }

                return optionsList;
            }
        }
    }
}
