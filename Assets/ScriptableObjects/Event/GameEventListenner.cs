using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameEventListenner
{
    void Trigger(GameEventData eventData);
}
