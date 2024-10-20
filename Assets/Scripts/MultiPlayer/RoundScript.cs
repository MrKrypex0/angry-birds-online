using DapperDino.Mirror.Tutorials.Lobby;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class RoundScript : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private GameObject currentCharacter;

    [SerializeField] private MultiPlayerAmo ammo1;
    [SerializeField] private MultiPlayerAmo ammo2;
    [SerializeField] private TextMeshProUGUI buildCountDownText;
    [SerializeField] private NetworkGamePlayerLobby gamePlayer1;
    [SerializeField] private NetworkGamePlayerLobby gamePlayer2;
    [SerializeField] private Inventory inventory1;
    [SerializeField] private Inventory inventory2;
    [SerializeField] private GameObject endGamesCanvas;
    [SerializeField] private TextMeshProUGUI endGameText;
    [SyncVar]
    [SerializeField] private GameObject player1BasePoint;
    [SyncVar]
    [SerializeField] private GameObject player2BasePoint;
    [SerializeField] private Animator endAnim;
    [SerializeField] private Canvas gamePlayCanvas;
    [SerializeField] private TextMeshProUGUI playerTurnText;
    [SerializeField] private Image timerBackground;

    [SyncVar]
    public bool player1 = false;
    [SyncVar]
    public bool player2 = false;
    [SyncVar]
    public bool GameStarted = false;

    [SyncVar]
    public bool startCountDown = false;
    [SyncVar]
    public float time = 120f;

    private void Start()
    {
        timerBackground.enabled = false;
        buildCountDownText.enabled = false;
        gamePlayCanvas.enabled = false;
    }

    private void Update()
    {
        if (gamePlayer1 == null || gamePlayer2 == null)
        {
            gamePlayer1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<NetworkGamePlayerLobby>();
            gamePlayer2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<NetworkGamePlayerLobby>();
        }

        if (inventory1 == null || inventory2 == null)
        {
            inventory1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Inventory>();
            inventory2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Inventory>();
        }

        if (GameStarted == true)
        {
            time = 0;
            buildCountDownText.gameObject.SetActive(false);
            gamePlayer1.StartGameFunction();
            gamePlayer2.StartGameFunction();
        }

        if (currentCharacter == null)
        {
            currentCharacter = GameObject.FindWithTag("Character1");
        }

        if (startCountDown == true)
        {
            timerBackground.enabled = true;
            buildCountDownText.enabled = true;
            buildCountDownText.text = "TimeLeft:" + " " + time.ToString("0");
            time -= Time.deltaTime;

            if (time <= 0f)
            {
                GameStarted = true;
                time = 0;
                startCountDown = false;
            }
        }

        CheckPlayerPoints();
        GameStartedFunction();

        if(GameStarted == true)
        {
            timerBackground.enabled = false;
            gamePlayCanvas.enabled = true;
        }

        if(player1 == true)
        {
            playerTurnText.text = "Player1" + " " + "Turn";
        }

        if (player2 == true)
        {
            playerTurnText.text = "Player2" + " " + "Turn";
        }
    }

    [ServerCallback]
    private void GameStartedFunction()
    {
        if (GameStarted == true)
        {
            if (currentCharacter == null && player1 == true)
            {
                player2 = true;
                player1 = false;
                CheckIfCharacterIsAlive();
                return;
            }

            if (currentCharacter == null && player2 == true)
            {
                player1 = true;
                player2 = false;
                CheckIfCharacterIsAlive();
                return;
            }
            CheckIfCharacterIsAlive();
        }
    }

    [ClientRpc]
    private void CheckIfCharacterIsAlive()
    {
        if (currentCharacter == null)
        {
            if (player1 == false && player2 == false)
            {
                player1 = true;
                StartRoundSystem();
                return;
            }
            StartRoundSystem();
        }
    }

    [ServerCallback]
    private void CheckPlayerPoints()
    {
        if (player2BasePoint == null)
        {
            RpcSetClientToDeafet();
        }

        if (player1BasePoint == null)
        {
            RpcSetClientToVictory();
        }
    }

    [ClientRpc]
    private void RpcSetClientToDeafet()
    {
        if (isClientOnly)
        {
            endGameText.text = "Defeat!";
            endGamesCanvas.SetActive(true);
            endAnim.SetTrigger("endgameMeny");
        }
        else
        {
            endGameText.text = "Victory!";
            endGamesCanvas.SetActive(true);
            endAnim.SetTrigger("endgameMeny");
        }
    }

    [ClientRpc]
    private void RpcSetClientToVictory()
    {
        if (isClientOnly)
        {
            endGameText.text = "Victory!";
            endGamesCanvas.SetActive(true);
            endAnim.SetTrigger("endgameMeny");
        }
        else
        {
            endGameText.text = "Defeat!";
            endGamesCanvas.SetActive(true);
            endAnim.SetTrigger("endgameMeny");
        }
    }

    private void StartRoundSystem()
    {
        if (isClientOnly == false)
        {
            if (player2 == true)
            {
                if (player1 == false)
                {
                    ammo2.RpcInstantiateCharacter();
                }
            }

            if (player1 == true)
            {
                if (player2 == false)
                {
                    ammo1.RpcInstantiateCharacter();
                }
            }
        }
    }
}
