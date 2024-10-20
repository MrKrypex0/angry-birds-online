using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Scoreboards;
using UnityEngine.UI;

public class WhatCharacter : MonoBehaviour
{
    public BuyScript buyScript;
    public MoneyController moneyController;
    public string whatCharacter;
    public int buyAmount;
    public bool clicked = false;
    public Button thisbutton;
    [SerializeField] bool disabled;
    [SerializeField] BuyIndication buyIndication;

    private void Start()
    {
        buyIndication = GameObject.FindGameObjectWithTag("buy").GetComponent<BuyIndication>();
    }

    private void Update()
    {   
        DisableButtonFunction();
    }


    public void WichCharacter()
    {
        int money;
        money = PlayerPrefs.GetInt("SavedHighscore")  - buyAmount;
        PlayerPrefs.SetInt("SavedHighscore", money);
        Debug.Log(PlayerPrefs.GetInt("SavedHighscore"));

        if (whatCharacter == "bombplayer")
        {
            PlayerPrefs.SetInt("BombPlayer", 1);
            Debug.Log(PlayerPrefs.GetInt("BombPlayer"));
        }

        if (whatCharacter == "darkplayer")
        {
            PlayerPrefs.SetInt("DarkPlayer", 1);
            Debug.Log(PlayerPrefs.GetInt("DarkPlayer"));
        }

        if (whatCharacter == "lavaplayer")
        {
            PlayerPrefs.SetInt("LavaPlayer", 1);
            Debug.Log(PlayerPrefs.GetInt("LavaPlayer"));
        }

        if (whatCharacter == "speedplayer")
        {
            PlayerPrefs.SetInt("SpeedPlayer", 1);
            Debug.Log(PlayerPrefs.GetInt("SpeedPlayer"));
        }

        if (whatCharacter == "spikeplayer")
        {
            PlayerPrefs.SetInt("SpikePlayer", 1);
            Debug.Log(PlayerPrefs.GetInt("SpikePlayer"));
        }

        if (whatCharacter == "treeplayer")
        {
            PlayerPrefs.SetInt("TreePlayer", 1);
            Debug.Log(PlayerPrefs.GetInt("TreePlayer"));
        }

        clicked = true;
        //buyScript.whatCharacter = whatCharacter;
        //moneyController.buyAmount = buyAmount;
        //buyScript.AddChampion();
        //moneyController.BuyFunction();
        //buyIndication.ownedPlayer = whatCharacter;
        //buyIndication.buyAmount = -buyAmount;

        PlayerPrefs.Save();
    }

    public void DisableButtonFunction()
    {
        if(whatCharacter == "bombplayer")
        {
            if(PlayerPrefs.GetInt("BombPlayer") == 1)
            {
                thisbutton.enabled = false;
                disabled = true;
            }
        }

        if (whatCharacter == "darkplayer")
        {
            if (PlayerPrefs.GetInt("DarkPlayer") == 1)
            {
                thisbutton.enabled = false;
                disabled = true;
            }
        }

        if (whatCharacter == "lavaplayer")
        {
            if (PlayerPrefs.GetInt("LavaPlayer") == 1)
            {
                thisbutton.enabled = false;
                disabled = true;
            }
        }

        if (whatCharacter == "speedplayer")
        {
            if (PlayerPrefs.GetInt("SpeedPlayer") == 1)
            {
                thisbutton.enabled = false;
                disabled = true;
            }
        }

        if (whatCharacter == "spikeplayer")
        {
            if (PlayerPrefs.GetInt("SpikePlayer") == 1)
            {
                thisbutton.enabled = false;
                disabled = true;
            }
        }

        if (whatCharacter == "treeplayer")
        {
            if (PlayerPrefs.GetInt("TreePlayer") == 1)
            {
                thisbutton.enabled = false;
                disabled = true;
            }
        }

        if (PlayerPrefs.GetInt("SavedHighscore") < buyAmount)
        {
            thisbutton.enabled = false;
            disabled = true;
        }
    }

}
