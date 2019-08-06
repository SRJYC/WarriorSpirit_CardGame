using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DeckBuilder
{
    public class DeckIO : MonoBehaviour
    {
        public const string directory = "DeckData";

        public CardCollection deckPrefab;

        public void Start()
        {
            Directory.CreateDirectory(directory);
        }

        public CardCollection ReadDeck(string name)
        {
            string path = directory + "/" + name + ".json";

            DeckData data;
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                data = JsonUtility.FromJson<DeckData>(json);
            }
            CardCollection deck = DataToCollection(data);

            return deck;
        }

        private CardCollection DataToCollection(DeckData data)
        {
            CardCollection deck = Instantiate(deckPrefab);

            deck.m_Warrior = AllUnitReference.Instance.GetCardById(data.warriorID, true);

            for (int i = 0; i < data.spiritsID.Length; i++)
            {
                int id = data.spiritsID[i];
                deck.m_CardList[i] = AllUnitReference.Instance.GetCardById(id);
            }

            return deck;
        }

        public void SaveDeck(CardCollection collection)
        {
            string path = directory + "/" + collection.name +".json";

            using (StreamWriter stream = new StreamWriter(path))
            {
                string json = JsonUtility.ToJson(CollectionToData(collection));
                stream.Write(json);
            }
        }

        private DeckData CollectionToData(CardCollection collection)
        {
            DeckData deckData = new DeckData();

            if (collection.m_Warrior != null)
                deckData.warriorID = collection.m_Warrior.ID;

            for(int i=0; i<deckData.spiritsID.Length; i++)
            {
                if (collection.m_CardList[i] != null)
                    deckData.spiritsID[i] = collection.m_CardList[i].ID;
            }

            return deckData;
        }
    }
}
