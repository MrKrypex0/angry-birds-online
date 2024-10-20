using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

namespace DapperDino.Mirror.Tutorials.Lobby
{
    public class GameManager : NetworkBehaviour
    {
        [SerializeField] private bool ready = false;

        public float timerCountDown;

        public TextMeshProUGUI countDownText;
        [SerializeField] CanvasGroup buildCanvas;

        public GameObject startCanvas;
        public GameObject selectionCanvas;


        private void Start()
        {
            if (!hasAuthority)
            {
                startCanvas.SetActive(false);
            }
            GetComponents();
        }

        [ClientRpc]
        void Update()
        {
            StartGameCode();
            StartGameFunction();
        }

        //Getting Startcomponents
        private void GetComponents()
        {
            //startComponents
        }

        //Check if players has joined
        private void StartGameCode()
        {
            StartCountDown();
        }

        //Start CountDown
        private void StartCountDown()
        {
            timerCountDown -= Time.deltaTime;

            countDownText.text = "Starting In:" + " " + timerCountDown.ToString("0");

            if (timerCountDown < 0)
            {
                StartBuildingFace();
                timerCountDown = 0;
            }
        }

        //Starting Building Face
        private void StartBuildingFace()
        {
            startCanvas.active = false;
            buildCanvas.gameObject.SetActive(true);
            buildCanvas.interactable = true;
            buildCanvas.blocksRaycasts = true;
            buildCanvas.alpha = 1;
        }

        //Starting BattleGame
        private void StartGameFunction()
        {
            if (ready == true)
            {
                buildCanvas.interactable = false;
                buildCanvas.blocksRaycasts = false;
                buildCanvas.alpha = 0;

                selectionCanvas.SetActive(true);
            }
        }

        [Command]
        public void BuildFinnished()
        {
            ready = !ready;
        }
    }
}

