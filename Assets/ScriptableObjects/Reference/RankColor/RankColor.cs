using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reference/RankColor")]
public class RankColor : SingletonScriptableObject<RankColor>
{
    public List<Color> m_ColorList;

    public Color GetColor(int rank)
    {
        if(rank>=0 && rank<m_ColorList.Count)
        {
            return m_ColorList[rank];
        }
        return default;
    }
}
