using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventData
{
   public T CastDataType<T>() where T:GameEventData
    {
        if (this.GetType() == typeof(T))
            return (T)this;
        else
            return null;
    }
}
