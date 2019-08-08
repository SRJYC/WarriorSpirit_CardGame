using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MainMenu
{
    public class LanguageChange : MonoBehaviour
    {
        private AllText m_Reference;

        public TMP_Dropdown m_Dropdown;

        public TextProperty m_Text;

        public void Start()
        {
            m_Reference = AllText.Instance;

            m_Dropdown.ClearOptions();

            List<string> list = new List<string>();
            foreach (AllText.Language language in System.Enum.GetValues(typeof(AllText.Language)))
            {
                list.Add(language.ToString());
            }

            m_Dropdown.AddOptions(list);

            m_Dropdown.value = PlayerPrefs.GetInt(AllText.PrefLanguageKey,0);
        }

        public void ChangeLanguage(int index)
        {
            PlayerPrefs.SetInt(AllText.PrefLanguageKey, index);
            Debug.Log(GameMessage.Instance);
            GameMessage.Instance.Display(m_Text.ToString());
        }
    }
}
