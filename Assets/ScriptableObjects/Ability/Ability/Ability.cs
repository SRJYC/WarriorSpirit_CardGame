using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Ability/Ability")]
public class Ability : ScriptableObject
{
    public bool AIPositive;

    #region All field
    [HideInInspector] public Unit m_Owner;

    [Header("Base Info")]
    public TextProperty m_AbilityName;
    public TextProperty m_Description;
    //public AbilityVariables m_Variables;

    [Header("Ability Info")]
    [Tooltip("Info[i] will store info of InfoGetter[i]")] public AbilityInfoGetter[] m_InfoGetters;
    [Tooltip("Info[i] will store info of InfoGetter[i]")] public AbilityInfo[] m_AbilityInfos;

    [Header("Cooldown")]
    public int m_CD = 0;
    [HideInInspector] public int m_CurrentCD = 0;

    [Header("Mana Cost")]
    public int m_ManaCost = 0;

    [Header("Active")]
    public bool m_IsAction = true;
    public bool m_IsActive = true;
    public bool m_IsDisplay = true;
    public ActiveListenner[] m_ActiveListenners;

    [Header("Conditions")]
    public ConditionComponent[] m_Conditions;

    [Header("Effects")]
    public EffectComponent[] m_Effects;

    [Header("Index Map")]
    [Tooltip("Index of [AbilityInfos] that will feed to each [effect].\n" +
        "Size should be the same as [m_Effects].\n" +
        "Each element in that array is the index of AbilityInfo")]
    public IntArrayRow[] m_EffectInfoIndices;

    [Tooltip("Index of [AbilityInfos] that will feed to each [condition].\n" +
        "Size should be the same as m_Conditions.\n" +
        "Each element in that array is the index of AbilityInfo")]
    public IntArrayRow[] m_ConditionInfoIndices;

    [Tooltip("Index of [Conditions] that will feed to each [effect].\n" +
        "Size should be the same as [m_Effects].\n" +
        "Each element in that array is the index of [Conditions]")]
    public IntArrayRow[] m_EffectConditionIndices;

    private bool m_HasCache = false;
    #endregion

    #region init

    /// <summary>
    /// /create clone of this Ability and all its Active listenners, info storage
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public Ability Clone(Unit unit)
    {
        Ability clone = Instantiate(this);

        clone.m_Owner = unit;

        for (int i = 0; i < clone.m_ActiveListenners.Length; i++)
        {
            clone.m_ActiveListenners[i] = Instantiate(clone.m_ActiveListenners[i]);
            clone.m_ActiveListenners[i].Init(clone);
        }
        for (int i = 0; i < clone.m_AbilityInfos.Length; i++)
        {
            clone.m_AbilityInfos[i] = Instantiate(clone.m_AbilityInfos[i]);
        }
        
        for(int i=0;i<m_Conditions.Length;i++)
        {
            clone.m_Conditions[i] = Instantiate(clone.m_Conditions[i]);
        }
        for (int i = 0; i < m_Effects.Length; i++)
        {
            clone.m_Effects[i] = Instantiate(clone.m_Effects[i]);
        }

        return clone;
    }

    #endregion

    #region Active
    private IEnumerator m_InfoGetterCoroutine;
    public void Trigger()
    {
        //Debug.Log("Try trigger : "+ m_IsActive);
        if (!m_IsActive)
            return;
        //Debug.Log(this+" start coroutine of gathering info");
        m_InfoGetterCoroutine = GatherInfo();
        CoroutineManager.Instance.StartCoroutine(m_InfoGetterCoroutine);
    }
    #endregion

    #region Get Info

    private int getterIndex;
    IEnumerator GatherInfo()
    {
        if (m_InfoGetters.Length != m_AbilityInfos.Length)
            Debug.LogError("Unmatched number of getters and infos.");

        getterIndex = 0;
        while( getterIndex < m_InfoGetters.Length)
        {
            m_InfoGetters[getterIndex].GetInfo(m_Owner);

            while (!m_InfoGetters[getterIndex].m_Done)
            {
                yield return new WaitForSeconds(.1f);
            }

            m_InfoGetters[getterIndex].StoreInfo(m_AbilityInfos[getterIndex]);

            getterIndex++;
        }

        if(m_IsAction)
            ActionManager.Instance.ActionConfirm();
        else
            TakeEffect();
    }

    public void CancelInfoGetter()
    {
        getterIndex--;
        //Debug.Log("Cancel Info Getter: " + getterIndex);
        if (getterIndex <= 0)
            CoroutineManager.Instance.StopCoroutine(m_InfoGetterCoroutine);
        else if (m_InfoGetters[getterIndex].m_Automatic)
        {
            //Debug.Log("Cancel Completed");
            CancelInfoGetter();
        }
        else
        {
            //Debug.Log("Reget");
            m_InfoGetters[getterIndex].GetInfo(m_Owner);
        }
    }

    public void HighlightInfo()
    {
        foreach(AbilityInfo info in m_AbilityInfos)
        {
            //Debug.Log(info + " highlight:");
            info.Highlight();
        }
    }

    public void UnhighlightInfo()
    {
        foreach (AbilityInfo info in m_AbilityInfos)
        {
            info.Unhighlight();
        }
    }
    #endregion

    #region Take Effect

    [ContextMenu("Take Effect")]
    public void TakeEffect()
    {
        if (!m_HasCache)
            MapIndices();

        for(int i=0; i<m_Effects.Length; i++)
        {
            m_Effects[i].TakeEffect();
        }

        m_CurrentCD = m_CD;

        if(m_ManaCost > 0)
            PlayerManager.Instance.GetPlayer(m_Owner.m_PlayerID).m_Mana.Cost(m_ManaCost);
    }

    private void MapIndices()
    {
        int size1 = m_Conditions.Length;
        for (int i = 0; i < size1; i++)
        {
            m_Conditions[i].Reset();

            //AbilityInfo list
            int[] indexList = m_ConditionInfoIndices[i].row;
            for (int j = 0; j < indexList.Length; j++)
            {
                int index = indexList[j];
                m_Conditions[i].Add(m_AbilityInfos[index]);
            }
        }

        int size2 = m_Effects.Length;
        for(int i=0; i<size2; i++)
        {
            m_Effects[i].Reset();

            //AbilityInfo list
            int[] indexList = m_EffectInfoIndices[i].row;
            for(int j=0;j<indexList.Length;j++)
            {
                int index = indexList[j];
                m_Effects[i].Add(m_AbilityInfos[index]);
            }

            //condition list
            indexList = m_EffectConditionIndices[i].row;
            for (int j = 0; j < indexList.Length; j++)
            {
                int index = indexList[j];
                m_Effects[i].Add(m_Conditions[index]);
            }
        }

        m_HasCache = true;
    }
    #endregion

}
