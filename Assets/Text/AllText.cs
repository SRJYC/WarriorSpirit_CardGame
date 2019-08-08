using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Text/AllText")]
public class AllText : SingletonScriptableObject<AllText>
{
    public enum Language
    {
        English,
        简体中文,
    }

    public string m_File;

    public Language m_Language;

    //id: ( language : text)
    public Dictionary<string, Dictionary<Language, string>> m_Map;

    private List<string> textID;

    public const string PrefLanguageKey = "Language";
    private const char delimiter = '\t';
    public void OnEnable()
    {
        Read();

        int index = PlayerPrefs.GetInt(PrefLanguageKey, 0);
        m_Language = (Language)index;
    }

    public void AddText(string id, string content, Language lan = Language.English)
    {
        if (m_Map == null)
            m_Map = new Dictionary<string, Dictionary<Language, string>>();

        Dictionary<Language, string> pairs;
        if(m_Map.TryGetValue(id, out pairs))
        {
            pairs[lan] = content;
        }
        else
        {
            pairs = new Dictionary<Language, string>();
            foreach (Language language in System.Enum.GetValues(typeof(Language)))
            {
                pairs.Add(language, "N/A");
            }

            pairs[lan] = content;

            m_Map.Add(id, pairs);
        }
    }

    public string GetText(string id)
    {
        string text = m_Map[id][m_Language];
        if (text == "" || text == "N/A")
            return m_Map[id][Language.English];
        else
            return text;
    }

    [ContextMenu("Export")]
    public void Write()
    {
        StreamWriter writer = new StreamWriter(m_File);

        //header
        writer.Write("ID" + delimiter);
        foreach(Language language in System.Enum.GetValues(typeof(Language)))
        {
            writer.Write(language.ToString()+ delimiter);
        }
        writer.WriteLine("");

        //content
        foreach (KeyValuePair<string,Dictionary<Language, string>> pair in m_Map)
        {
            writer.Write(pair.Key + delimiter);

            foreach(string text in pair.Value.Values)
            {
                writer.Write(text + delimiter);
            }

            writer.WriteLine("");
        }

        writer.Close();
        Debug.Log("Finish Writing");
    }

    [ContextMenu("Import")]
    public void Read()
    {
        m_Map = new Dictionary<string, Dictionary<Language, string>>();

        StreamReader reader = new StreamReader(m_File);

        //header
        reader.ReadLine();

        //content
        System.Array languages = System.Enum.GetValues(typeof(Language));

        string line = reader.ReadLine();
        while(line != null)
        {
            string[] values = line.Split(delimiter);

            //id
            int index = 0;
            string id = values[index];

            Dictionary<Language, string> pairs;
            if (!m_Map.TryGetValue(id, out pairs))
            {
                pairs = new Dictionary<Language, string>();
                m_Map.Add(id, pairs);
            }

            foreach (Language language in languages)
            {
                index++;
                pairs[language] = values[index];
            }

            line = reader.ReadLine();
        }

        reader.Close();
    }
}
