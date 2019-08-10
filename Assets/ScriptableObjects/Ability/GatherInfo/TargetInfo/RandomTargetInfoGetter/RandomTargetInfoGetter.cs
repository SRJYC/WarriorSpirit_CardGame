using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityInfoGetter/RandomTargetGetter")]
public class RandomTargetInfoGetter : TargetInfoGetter
{
    [Header("Select Range")]
    public List<FixedTargetInfoGetter> m_AvaliableTargetGetter;
    [HideInInspector] public List<FieldBlock> m_AvaliableBlocks;

    [Header("Random Select")]
    public int m_Num;
    public bool m_CanRepeat;

    public override void GetInfo(Unit source)
    {
        m_Done = false;

        Reset();

        GetAvaliableTargets(source);

        m_Blocks = RandomSelect();

        Filter();

        m_Done = true;
    }

    private List<FieldBlock> RandomSelect()
    {
        List<FieldBlock> list = new List<FieldBlock>();

        int size = m_AvaliableBlocks.Count;
        List<int> indices = new List<int>();
        for (int i = 0; i < m_Num; i++)
        {
            int index = Random.Range(0, size);
            while (!m_CanRepeat && indices.Contains(index))
            {
                index = Random.Range(0, size);
            }
            indices.Add(index);
            list.Add(m_AvaliableBlocks[index]);
        }

        return list;
    }

    private void GetAvaliableTargets(Unit source)
    {
        m_AvaliableBlocks = new List<FieldBlock>();
        foreach (FixedTargetInfoGetter infoGetter in m_AvaliableTargetGetter)
        {
            infoGetter.GetInfo(source);
            m_AvaliableBlocks.AddRange(infoGetter.m_Blocks);
        }
    }
}
