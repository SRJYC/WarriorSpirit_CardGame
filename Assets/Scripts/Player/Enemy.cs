using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Singleton<Enemy>
{
    public PlayerID m_ID { get; private set; }

    public void Init(PlayerID id)
    {
        m_ID = id;
    }
}
