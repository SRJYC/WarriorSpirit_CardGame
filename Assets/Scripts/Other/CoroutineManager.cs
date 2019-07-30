using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager m_Instance;
    public static CoroutineManager Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<CoroutineManager>();

            return m_Instance;
        }
        private set
        {
            m_Instance = value;
        }
    }
}
