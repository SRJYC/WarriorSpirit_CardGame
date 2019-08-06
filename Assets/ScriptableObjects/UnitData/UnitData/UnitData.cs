using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Unit/UnitData")]
public class UnitData : ScriptableObject
{
    #region field
    [Header("Reference to Origin Data (no clone)")]
    [HideInInspector] public UnitData m_OriginData = null;

    [Header("Reference to Constant Data (no clone)")]
    [SerializeField] private ConstantUnitData m_ConstantData = null;

    [Header("Spirit Types")]
    [SerializeField] private List<SpiritType> m_SpiritTypes = null;

    [Header("Abilities(will clone)")]
    public List<Ability> m_Abilities = new List<Ability>();

    [Header("Stats")]
    public GameEvent m_StatsChangedEventForDisplay;
    public UnitEvent m_StatsChangedEventForUnits;

    public int m_Cost = 0;
    public int m_Durability = 0;
    public int m_Power = 0;
    public int m_Speed = 0;

    [Header("Optional Custom Stats")]
    public List<UnitStatsProperty> unitStats;
    public List<int> valueForStats;

    private Dictionary<UnitStatsProperty, int> m_Stats = null;

    public Unit m_Owner { get; private set; }

    private const int BASE_LEVEL = 0;

    #endregion

    public void Init(Unit unit)
    {
        m_Owner = unit;
        //m_OriginData = origin;

        InitStats();

        for (int i = 0; i < m_Abilities.Count; i++)
        {
            m_Abilities[i] = m_Abilities[i].Clone(unit);
        }
    }

    private void InitStats()
    {
        //Debug.Log("Init Stats");

        m_Stats = new Dictionary<UnitStatsProperty, int>();

        if (!m_ConstantData.m_IsWarrior)
            m_Stats.Add(UnitStatsProperty.LV, BASE_LEVEL);

        if (!m_ConstantData.m_IsWarrior)
        {
            m_Stats.Add(UnitStatsProperty.Cost, m_Cost);
        }

        m_Stats.Add(UnitStatsProperty.DUR, m_Durability);
        m_Stats.Add(UnitStatsProperty.POW, m_Power);
        m_Stats.Add(UnitStatsProperty.SPD, m_Speed);


        for(int i=0; i<unitStats.Count;i++)
        {
            m_Stats.Add(unitStats[i], valueForStats[i]);
        }
    }

    public UnitData Clone(Unit unit)
    {
        UnitData clone = Instantiate(this);

        clone.Init(unit);

        clone.m_OriginData = this;

        return clone;
    }

    #region access data
    public int ID { get { return m_ConstantData.ID; } }
    public string UnitName { get { return m_ConstantData.m_UnitName.ToString(); } }
    public string Description { get { return m_ConstantData.m_Description.ToString(); } }
    public Sprite Artwork { get { return m_ConstantData.m_Artwork; } }
    public bool IsWarrior { get { return m_ConstantData.m_IsWarrior; } }
    public bool CanBeFrontline { get { return m_ConstantData.m_CanBeFront; } }
    public bool CanBeBackline { get { return m_ConstantData.m_CanBeBack; } }
    public bool IsUnique { get { return m_ConstantData.m_Unique; } }
    public int Rank { get { return m_ConstantData.m_Rank; } }
    public List<RankUpCondition> m_RankUps { get { return m_ConstantData.m_RankUps; } }

    public List<SpiritType> GetSpiritTypes()
    {
        List<SpiritType> list = new List<SpiritType>();
        list.AddRange(m_SpiritTypes);

        if(m_Owner != null)
            list.AddRange(m_Owner.m_Status.m_SpiritTypes);

        return list;
    }

    public bool IsSpiritType(SpiritType type)
    {
        if (m_SpiritTypes.Contains(type))
            return true;
        else if (m_Owner != null)
            return m_Owner.m_Status.m_SpiritTypes.Contains(type);
        else
            return false;
    }

    public Dictionary<UnitStatsProperty, int> GetAllStat()
    {
        Dictionary<UnitStatsProperty, int> result = new Dictionary<UnitStatsProperty, int>();

        if (m_Stats == null)
            InitStats();

        foreach (var property in m_Stats)
        {
            result.Add(property.Key, property.Value);
        }

        if (m_Owner != null)
        {
            Dictionary<UnitStatsProperty, int> statusStats = m_Owner.m_Status.GetAllStat();

            foreach (var property in statusStats)
            {
                if (result.ContainsKey(property.Key))
                    result[property.Key] += property.Value;
                else
                    result.Add(property.Key, property.Value);
            }
        }

        return result;
    }

    public int GetStat(UnitStatsProperty stat)
    {
        if (m_Stats == null)
            InitStats();

        int value;
        m_Stats.TryGetValue(stat, out value);

        //Debug.Log(m_Owner + " has [" + stat + ":" + value + "]");

        if(m_Owner != null )
        {
            int deltaValue = m_Owner.m_Status.GetStat(stat);
            value += deltaValue;
        }

        return value;
    }

    /// <summary>
    /// Change unit's stats.
    /// </summary>
    /// <param name="stat">The field to change</param>
    /// <param name="amount">The value of change</param>
    /// <param name="set">True if replace original value with amount. False if original value += amount</param>
    /// <param name="force">True if add new field when needed</param>
    public void ChangeStats(UnitStatsProperty stat, int amount, bool set = false, bool force = false)
    {
        if (m_Owner == null)
            return;

        //Debug.Log(m_Owner + " " + stat + " : " + GetStat(stat));
        UnitStatsChangeData eventData = new UnitStatsChangeData();
        eventData.m_Unit = m_Owner;
        eventData.m_ChangedStats = stat;

        eventData.m_BeforeChange = GetStat(stat);

        PrivateChangeStats(stat,amount, set, force);

        eventData.m_AfterChange = GetStat(stat);

        m_StatsChangedEventForDisplay.Trigger(eventData);
        m_StatsChangedEventForUnits.Trigger(m_Owner, eventData);
        //Debug.Log(m_Owner + " " + stat + " Change to: " + GetStat(stat));
    }

    private void PrivateChangeStats(UnitStatsProperty stat, int amount, bool set, bool force)
    {
        TriggerSFX(amount);

        if (m_Stats.ContainsKey(stat))
        {
            if (set)
            {
                m_Stats[stat] = amount;
            }
            else
            {
                m_Stats[stat] += amount;
            }
        }
        else if (force)
        {
            m_Stats.Add(stat, amount);
        }
        else
            Debug.LogError("Can't Change " + stat + ": Stats Doesn't Exist");
    }

    private static void TriggerSFX(int amount)
    {
        if (amount >= 0)
            AudioManager.Instance.Play(SoundEffectType.Postive);
        else
            AudioManager.Instance.Play(SoundEffectType.Negative);
    }
    #endregion
}
