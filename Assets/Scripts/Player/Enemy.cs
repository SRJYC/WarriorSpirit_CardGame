using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Singleton<Enemy>
{
    public enum Type
    {
        AI,
        NetPlayer,
    }
    public Type m_Type;

    public delegate void ConfirmCallback();
    public ConfirmCallback confirmCallback;

    public void Confirm(ConfirmCallback callback)
    {
        if (m_Type == Type.AI)
            callback.Invoke();
    }
}
