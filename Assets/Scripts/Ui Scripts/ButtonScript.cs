using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.Scoreboards;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{
    [Header("Values")]

    [SerializeField] int maxClickAmount = 4;
    [SerializeField] static int clickamount;
    [SerializeField] bool start = false;
    [SerializeField] bool disabled;
    public string WhatButton;
    public int clickCkeck;

    [Header("Components")]
    [SerializeField] Button slotButton;
    [SerializeField] AddAmmo addAmmo;
    [SerializeField] GameObject canvas;
    [SerializeField] ImageDisplay image1;
    [SerializeField] ImageDisplay image2;
    [SerializeField] ImageDisplay image3;
    [SerializeField] ImageDisplay image4;
    [SerializeField] ImageDisplay image5;
    public Animator anim;
    public GameObject blackBackgroundImage;
    public ScoreboardSaveData savedScore;


    private string SavePath => $"{Application.persistentDataPath}/OwnedChampions.json";

    void Start()
    {
        StartCode();
        savedScore = GetSavedScores();
        slotButton.enabled = false;
    }

    private void StartCode()
    {
        addAmmo = GameObject.FindGameObjectWithTag("AMO").GetComponent<AddAmmo>();
        canvas = GameObject.FindGameObjectWithTag("SelectionCanvas");
        image1 = GameObject.FindGameObjectWithTag("Image1").GetComponent<ImageDisplay>();
        image2 = GameObject.FindGameObjectWithTag("Image2").GetComponent<ImageDisplay>();
        image3 = GameObject.FindGameObjectWithTag("Image3").GetComponent<ImageDisplay>();
        image4 = GameObject.FindGameObjectWithTag("Image4").GetComponent<ImageDisplay>();
        image5 = GameObject.FindGameObjectWithTag("Image5").GetComponent<ImageDisplay>();
        anim = GameObject.FindGameObjectWithTag("SelectionCanvas").GetComponent<Animator>();
    }

    private void Update()
    {
        DisableThisButton();
        clickCkeck = clickamount;

        if(clickCkeck >= maxClickAmount)
        {
            slotButton.enabled = false;
            disabled = true;
        }

        SetIndexToImageDisplay();
    }

    private void SetIndexToImageDisplay()
    {
        if (clickCkeck == 1)
        {
            image1.index = clickCkeck;
        }

        if (clickCkeck == 2)
        {
            image2.index = clickCkeck;
        }

        if (clickCkeck == 3)
        {
            image3.index = clickCkeck;
        }

        if (clickCkeck == 4)
        {
            image4.index = clickCkeck;
        }

        if (clickCkeck == 5)
        {
            image5.index = clickCkeck;
        }
    }

    public void AddCharacters()
    {
        if (WhatButton == "astroplayer")
        {
            WhatTypeOfButton();

            if (clickamount <= maxClickAmount)
            {
                clickamount++;
                addAmmo.astroPlayerIndex++;
            }

            if (clickamount == maxClickAmount)
            {
                slotButton.enabled = false;
            }
        }

        if (WhatButton == "treeplayer")
        {
            WhatTypeOfButton();

            if(clickamount <= maxClickAmount)
            {
                clickamount++;
                addAmmo.treePlayerIndex++;
            }

            if(clickamount == maxClickAmount)
            {
                slotButton.enabled = false;
            }
        }

        if (WhatButton == "lavaplayer")
        {
            WhatTypeOfButton();

            if (clickamount <= maxClickAmount)
            {
                clickamount++;
                addAmmo.lavaPlayerIndex++;
            }

            if (clickamount == maxClickAmount)
            {
                slotButton.enabled = false;
            }
        }

        if (WhatButton == "spikeplayer")
        {
            WhatTypeOfButton();

            if (clickamount <= maxClickAmount)
            {
                clickamount++;
                addAmmo.spikePlayerIndex++;
            }

            if (clickamount == maxClickAmount)
            {
                slotButton.enabled = false;
            }
        }

        if (WhatButton == "darkplayer")
        {
            WhatTypeOfButton();

            if (clickamount <= maxClickAmount)
            {
                clickamount++;
                addAmmo.darkPlayerIndex++;
            }

            if (clickamount == maxClickAmount)
            {
                slotButton.enabled = false;
            }
        }

        if (WhatButton == "speedplayer")
        {
            WhatTypeOfButton();

            if (clickamount <= maxClickAmount)
            {
                clickamount++;
                addAmmo.speedPlayerIndex++;
            }

            if (clickamount == maxClickAmount)
            {
                slotButton.enabled = false;
            }
        }

        if (WhatButton == "bombplayer")
        {
            WhatTypeOfButton();

            if (clickamount <= maxClickAmount)
            {
                clickamount++;
                addAmmo.bombPlayerIndex++;
            }

            if (clickamount == maxClickAmount)
            {
                slotButton.enabled = false;
            }
        }
    }

    private void WhatTypeOfButton()
    {
        if (clickamount <= 1)
        {
            image1.buttonType = WhatButton;
        }

        if (clickamount <= 2)
        {
            image2.buttonType = WhatButton;
        }

        if (clickamount <= 3)
        {
            image3.buttonType = WhatButton;
        }

        if (clickamount <= 4)
        {
            image4.buttonType = WhatButton;
        }

        if (clickamount <= 5)
        {
            image5.buttonType = WhatButton;
        }
    }

    public void StartButton()
    {
        start = true;
        if (clickCkeck == maxClickAmount && start == true)
        {
            blackBackgroundImage.SetActive(false);
            anim.SetTrigger("SelectionEnd");
            addAmmo.AddCharacters();
            addAmmo.InstantiateAllCharacters();
            clickamount = 0;
            clickCkeck = 0;
        }
    }

    public void DisableThisButton()
    {
        if(WhatButton == "astroplayer")
        {
            slotButton.enabled = true;
        }

        if(WhatButton == "bombplayer")
        {
           if(PlayerPrefs.GetInt("BombPlayer") == 1)
           {
                slotButton.enabled = true;
           }
        }

        if (WhatButton == "darkplayer")
        {
            if (PlayerPrefs.GetInt("DarkPlayer") == 1)
            {
                slotButton.enabled = true;
            }
        }

        if (WhatButton == "lavaplayer")
        {
            if (PlayerPrefs.GetInt("LavaPlayer") == 1)
            {
                slotButton.enabled = true;
            }
        }

        if (WhatButton == "speedplayer")
        {
            if (PlayerPrefs.GetInt("SpeedPlayer") == 1)
            {
                slotButton.enabled = true;
            }
        }

        if (WhatButton == "spikeplayer")
        {
            if (PlayerPrefs.GetInt("SpikePlayer") == 1)
            {
                slotButton.enabled = true;
            }
        }

        if (WhatButton == "treeplayer")
        {
            if (PlayerPrefs.GetInt("TreePlayer") == 1)
            {
                slotButton.enabled = true;
            }
        }


        //for(int i = 0; i < savedScore.highscores.Count; i++)
        //{
        //    if(savedScore.highscores[i].entryName == WhatButton)
        //    {
        //        slotButton.enabled = true;
        //    }
        //}
    }

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
}
