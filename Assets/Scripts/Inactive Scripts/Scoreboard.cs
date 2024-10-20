using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Scoreboards
{
    public class Scoreboard : MonoBehaviour
    {
        [SerializeField] private int maxScoreboardEntries = 5;
        [SerializeField] private Transform highscoresHolderTransform = null;
        [SerializeField] private GameObject scoreboardEntryObject = null;
        public string usage;

        [Header("Test")]
        public string testEntryName;
        [SerializeField] private int testEntryScore;

        private string SavePath => $"{Application.persistentDataPath}/highscores.json";

        private void Start()
        {
            StartCode();
        }

        private void StartCode()
        {
            /*if (PlayerPrefs.GetString("PlayerName") != null && PlayerPrefs.GetInt("Highscore") != 0)
            {
                testEntryName = PlayerPrefs.GetString("PlayerName");
                testEntryScore = PlayerPrefs.GetInt("Highscore");
            }*/
            // AddTestEntry();
            ScoreboardSaveData savedScores = GetSavedScores();

            //UpdateUI(savedScores);

            SaveScores(savedScores);
        }

        [ContextMenu("Add Test Entry")]
        public void AddTestEntry()
        {
           /*AddEntry(new ScoreboardEntryData()
            {
                entryName = testEntryName,
                entryScore = testEntryScore
            }); */
        }

        public void AddEntry(ScoreboardEntryData scoreboardEntryData)
        {
            if(testEntryScore != 0)
            {
                ScoreboardSaveData savedScores = GetSavedScores();

                bool scoreAdded = false;

                //Check if the score is high enough to be added.
                for (int i = 0; i < savedScores.highscores.Count; i++)
                {
                    savedScores.highscores.Insert(i, scoreboardEntryData);
                    scoreAdded = true;
                    break; 
                } 

                //Check if the score can be added to the end of the list.
                if (!scoreAdded && savedScores.highscores.Count < maxScoreboardEntries)
                {
                    savedScores.highscores.Add(scoreboardEntryData);
                } 

                //Remove any scores past the limit.
                if (savedScores.highscores.Count > maxScoreboardEntries)
                {
                    savedScores.highscores.RemoveRange(maxScoreboardEntries, savedScores.highscores.Count - maxScoreboardEntries);
                } 

                //UpdateUI(savedScores);

                SaveScores(savedScores);
            }
            
        }

       /* private void UpdateUI(ScoreboardSaveData savedScores)
        {
            foreach (Transform child in highscoresHolderTransform)
            {
                Destroy(child.gameObject);
            }

            
            foreach (ScoreboardEntryData highscore in savedScores.highscores)
            {
                Instantiate(scoreboardEntryObject, highscoresHolderTransform).GetComponent<ScoreboardEntryUI>().Initialise(highscore);
            }
             
        } */

        public ScoreboardSaveData GetSavedScores()
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

        public void SaveScores(ScoreboardSaveData scoreboardSaveData)
        {
            using (StreamWriter stream = new StreamWriter(SavePath))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json);
            }
        }
    }
}
