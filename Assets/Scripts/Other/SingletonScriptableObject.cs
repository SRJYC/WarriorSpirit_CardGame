using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
{
    static T _instance = null;
    public static T Instance
    {
        get
        {   
            if (_instance == null)
            {
                //Debug.Log(typeof(T).Name);
                //_instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
                _instance = Resources.Load(typeof(T).Name) as T;
                //Debug.Log(_instance);
            }

            return _instance;
        }
    }
}