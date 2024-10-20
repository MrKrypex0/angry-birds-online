using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.Scoreboards
{
    public class Score : MonoBehaviour
    {

        [Header("Test")]
        [SerializeField] private int testEntryScore;
        [SerializeField] MoneyController moneyController;
        public TextMeshProUGUI entryScoreText;
        public ScoreboardSaveData savedScore;
        public string whatScene;
        public int currentScore;

        private string SavePath1 => $"{Application.persistentDataPath}/score.json";

        private void Start()
        {
            currentScore = PlayerPrefs.GetInt("SavedHighscore");
            Debug.Log(PlayerPrefs.GetInt("SavedHighscore"));

            GetStartScore();
            GetStartComponents();
            StartCode();
        }

        private void GetStartComponents()
        {
            if (whatScene == "store")
            {
                moneyController = GameObject.FindGameObjectWithTag("buyscript").GetComponent<MoneyController>();
            }
            else
            {
                moneyController = null;
            }
        }

        private void GetStartScore()
        {
            if(PlayerPrefs.GetInt("Highscore") != 0)
            {
                currentScore = PlayerPrefs.GetInt("Highscore") + PlayerPrefs.GetInt("SavedHighscore");
                PlayerPrefs.SetInt("SavedHighscore", currentScore);
                PlayerPrefs.Save();
            }


        }

        private void StartCode()
        {
            StartingFunction();

            RemoveOtherInputs();

            UpdateUIFunction();
        }

        private void RemoveOtherInputs()
        {
            if(savedScore.highscores.Count > 1)
            {
                savedScore.highscores.Remove(savedScore.highscores[0]);
            }
        }

        private void StartingFunction()
        {
            currentScore = PlayerPrefs.GetInt("SavedHighscore");
            PlayerPrefs.DeleteKey("Highscore");

            //if (whatScene == "AddScoreScene")
            //{
            //    if (PlayerPrefs.GetInt("HasPlayed") == 1)
            //    {
            //        for (int i = 0; i < savedScore.highscores.Count; i++)
            //        {
            //            if (savedScore.highscores.Count > 0)
            //            {
            //               // savedScore.highscores[i].entryScore +

            //                testEntryScore = currentScore;
            //            }
            //        }
            //       // AddScore();
            //    }
            //}
        }

        public void AddScore()
        {
            AddScoreFunction(new ScoreboardEntryData()
            {
                entryScore = testEntryScore
            });
        }

        public void AddScoreFunction(ScoreboardEntryData scoreboardEntryData)
        {
            savedScore.highscores.Add(scoreboardEntryData);

            SaveScores(savedScore);


            PlayerPrefs.DeleteKey("HasPlayed");
            PlayerPrefs.Save();
            UpdateUIFunction();
        }

        public void UpdateUIFunction()
        {
            StartCoroutine(UpdateScoreText());
        }

        IEnumerator UpdateScoreText()
        {
            entryScoreText.text = currentScore.ToString();

            //SaveScores(savedScore);
            yield return new WaitForSeconds(0.5f);
            //savedScore = GetSavedScores();
            //for (int i = 0; i < savedScore.highscores.Count; i++)
            //{
            //    //currentScore = savedScore.highscores[i].entryScore;

            //    if(whatScene == "store")
            //    {
            //        moneyController.testEntryScore = currentScore;
            //    }
            //}
        }

        private ScoreboardSaveData GetSavedScores()
        {
            if (!File.Exists(SavePath1))
            {
                File.Create(SavePath1).Dispose();

                AddScoreFunction(new ScoreboardEntryData()
                {
                    entryScore = 0
                });

                return new ScoreboardSaveData();
            }

            using (StreamReader stream = new StreamReader(SavePath1))
            {
                string json = stream.ReadToEnd();

                return JsonUtility.FromJson<ScoreboardSaveData>(json);
            }
        }

        private void SaveScores(ScoreboardSaveData scoreboardSaveData)
        {
            using (StreamWriter stream = new StreamWriter(SavePath1))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json);
            }
        }
    }
}
