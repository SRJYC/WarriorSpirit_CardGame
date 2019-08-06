using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace DeckBuilder
{
    public class DeckBuilder : MonoBehaviour
    {
        public DeckIO m_IO;

        public const string File = "/AllDeckIndex.json";

        [HideInInspector] public DeckList m_DeckList;
        public List<CardCollection> m_Decks = new List<CardCollection>();

        [Header("Display Deck Labels")]
        public List<DeckSelect> m_DeckSelects;

        [Header("Display Current Deck")]
        public DeckDisplay m_Display;
        public CardCollection m_CurrentSelectDeck;
        private DeckSelect m_CurrentSelect;

        void Start()
        {
            Read();

            CreateCollection();

            DisplayDeckNames();

            DefaultDeckDisplay();
        }

        private void DefaultDeckDisplay()
        {
            Select(m_DeckSelects[0]);
        }

        private void DisplayDeckNames()
        {
            for (int i = 0; i < m_DeckSelects.Count; i++)
            {
                m_DeckSelects[i].Init(this, m_Decks[i]);
            }
        }

        private void CreateCollection()
        {
            foreach (string deckName in m_DeckList.m_DeckNames)
            {
                CardCollection deck = m_IO.ReadDeck(deckName);
                deck.name = deckName;
                m_Decks.Add(deck);
            }
        }

        internal void Select(DeckSelect deckSelect)
        {
            if(m_CurrentSelect != null)
                m_CurrentSelect.ChangeSelect(false);

            m_Display.ChangeDeck(deckSelect.m_Deck);
            deckSelect.ChangeSelect(true);

            m_CurrentSelect = deckSelect;
        }

        public void SaveAllDecks()
        {
            /*
            //delete all files
            DirectoryInfo di = new DirectoryInfo(DeckIO.directory);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }*/

            //save index
            Write();

            //save all decks
            foreach(CardCollection collection in m_Decks)
            {
                m_IO.SaveDeck(collection);
            }

            OverwriteSelect(m_CurrentSelect.m_Deck);
        }

        private void Read()
        {
            string path = DeckIO.directory + File;
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                m_DeckList = JsonUtility.FromJson<DeckList>(json);
            }
        }

        private void Write()
        {
            string path = DeckIO.directory + File;
            using (StreamWriter stream = new StreamWriter(path))
            {
                string json = JsonUtility.ToJson(m_DeckList);
                //Debug.Log(json);
                stream.Write(json);
            }
        }

        private void OverwriteSelect(CardCollection collection)
        {
            m_CurrentSelectDeck.m_Warrior = collection.m_Warrior;
            m_CurrentSelectDeck.m_CardList = collection.m_CardList;
        }
    }
}
