using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI.Scoreboards
{
    public class AddFreeChampion : MonoBehaviour
    {
        private string SavePath => $"{Application.persistentDataPath}/OwnedChampions.json";
        public ScoreboardSaveData savedOwnChampions;
        public string whatCharacter;

        void Awake()
        {
            savedOwnChampions = SavedChampions();
            if(savedOwnChampions.highscores.Count == 0)
            {
                AddFreeChampionFunction();
            }
        }

        private void AddFreeChampionFunction()
        {
            AddEntry(new ScoreboardEntryData()
            {
                entryName = whatCharacter
            });
        }

        public void AddEntry(ScoreboardEntryData scoreboardEntryData)
        {

            savedOwnChampions.highscores.Add(scoreboardEntryData);

            SavedChampions(savedOwnChampions);
        }

        private void SavedChampions(ScoreboardSaveData scoreboardSaveData)
        {
            using (StreamWriter stream = new StreamWriter(SavePath))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json);
            }
        }

        public ScoreboardSaveData SavedChampions()
        {
            if (!File.Exists(SavePath))
            {
                File.Create(SavePath).Dispose();
                return new ScoreboardSaveData();
            }

            using (StreamReader stream = new StreamReader(SavePath))
            {
                string json = stream.ReadToEnd();

                return JsonUtility.FromJson<ScoreboardSaveData>(json);
            }
        }
    }
}

