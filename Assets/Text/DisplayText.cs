using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayText : MonoBehaviour
{
    public List<TextProperty> m_label;
    public string m_conntector;
    public TMPro.TextMeshProUGUI m_Text;

    // Start is called before the first frame update
    void Start()
    {
        string s = "";
        ParseConnector();

        foreach (TextProperty property in m_label)
        {
            s += property.ToString() + m_conntector;
        }

        m_Text.text = s;
    }

    private void ParseConnector()
    {
        m_conntector = m_conntector.Replace("\\n", "\n");
        m_conntector = m_conntector.Replace("\\t", "\t");
    }
}
