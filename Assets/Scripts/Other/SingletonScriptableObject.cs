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
                string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
                if(guids.Length != 0)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    _instance = AssetDatabase.LoadAssetAtPath<T>(path);
                }
            }

            return _instance;
        }
    }
}