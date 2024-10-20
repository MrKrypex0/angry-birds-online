using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI.Scoreboards
{
    public class MoneyController : MonoBehaviour
    {
        [Header("Test")]
        public int testEntryScore;
        public int buyAmount;
        public ScoreboardSaveData savedMoney;
        private string SavePath1 => $"{Application.persistentDataPath}/score.json";

        private void Start()
        {
            savedMoney = GetSavedMoney();

            StartFunction();
        }

        private void StartFunction()
        {
            for (int i = 0; i < savedMoney.highscores.Count; i++)
            {
                testEntryScore = savedMoney.highscores[i].entryScore;
            }
        }

        public void BuyFunction()
        {
            savedMoney = GetSavedMoney();
            BuyFunction(new ScoreboardEntryData()
            {
                entryScore = testEntryScore - buyAmount
            });
        }

        public void BuyFunction(ScoreboardEntryData scoreboardEntryData)
        {
            savedMoney.highscores.Add(scoreboardEntryData);
            SaveMoney(savedMoney);
        }

        public ScoreboardSaveData GetSavedMoney()
        {
            if (!File.Exists(SavePath1))
            {
                File.Create(SavePath1).Dispose();
                return new ScoreboardSaveData();
            }

            using (StreamReader stream = new StreamReader(SavePath1))
            {
                string json = stream.ReadToEnd();

                return JsonUtility.FromJson<ScoreboardSaveData>(json);
            }
        }

        private void SaveMoney(ScoreboardSaveData scoreboardSaveData)
        {
            using (StreamWriter stream = new StreamWriter(SavePath1))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json);
            }
        }
    }
}