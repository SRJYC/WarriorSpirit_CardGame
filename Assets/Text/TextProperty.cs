using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Text/Text")]
public class TextProperty : ScriptableObject
{
    //public string id;

    [TextArea(1,10)]
    public string text;

    public override string ToString()
    {
        AllText all = AllText.Instance;
        if (all == null)
            return text;
        else
            return all.GetText(name);
    }

    [ContextMenu("Write")]
    public void Write()
    {
        AllText.Instance.AddText(name, text);
    }
}
