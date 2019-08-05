using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ManaDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;

    private ManaObject mana;

    public void Init(ManaObject manaObject)
    {
        mana = manaObject;
    }

    private void Update()
    {
        if(mana != null)
        {
            text.text = mana.currentMana + "/" + mana.maxMana;
        }
    }
}
