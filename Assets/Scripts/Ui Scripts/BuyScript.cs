using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI.Scoreboards
{
    public class BuyScript : MonoBehaviour
    {

        [Header("Test")]
        public string whatCharacter;
        public List<String> ownedChampoins;
        public ScoreboardSaveData savedOwnChampions;
        [SerializeField] Score scoreScript;

        private string SavePath2 => $"{Application.persistentDataPath}/OwnedChampions.json";

        private void Start()
        {
            //savedOwnChampions = GetSavedChampions();

            for(int i = 0; i < savedOwnChampions.highscores.Count; i++)
            {
               ownedChampoins.Add(savedOwnChampions.highscores[i].entryName);
            }
        }

        public void AddChampion()
        {
            AddChampionFunction(new ScoreboardEntryData()
            {
                entryName = whatCharacter
            }) ;
        }

        public void AddChampionFunction(ScoreboardEntryData scoreboardEntryData)
        {
              savedOwnChampions.highscores.Add(scoreboardEntryData);
              ownedChampoins.Add(whatCharacter);
              SaveChampions(savedOwnChampions);
        }

        public ScoreboardSaveData GetSavedChampions()
        {
            if (!File.Exists(SavePath2))
            {
                File.Create(SavePath2).Dispose();
                return new ScoreboardSaveData();
            }

            using (StreamReader stream = new StreamReader(SavePath2))
            {
                string json = stream.ReadToEnd();

                return JsonUtility.FromJson<ScoreboardSaveData>(json);
            }
        }

        public void SaveChampions(ScoreboardSaveData scoreboardSaveData)
        {
            using (StreamWriter stream = new StreamWriter(SavePath2))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json);
            }
            scoreScript.UpdateUIFunction();
        }
    }
}

