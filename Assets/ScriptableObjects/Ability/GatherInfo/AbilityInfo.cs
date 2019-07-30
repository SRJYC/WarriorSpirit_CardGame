using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityInfo : ScriptableObject
{
    public T CastInfoType<T>() where T:AbilityInfo
    {
        if (this.GetType() == typeof(T))
            return (T)this;
        else
            Debug.LogError("Invalid Type of Info");

        return null;
    }


    public virtual void Highlight()
    {
        //Debug.Log("No highlight");
    }


    public virtual void Unhighlight()
    {

    }
}
