using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatSelectionButton : MonoBehaviour
{
    [Header("Values")]
    public string whatButton;
    public int SceneNumber;

    [Header("Components")]
    [SerializeField] Fader fader;

    private void Start()
    {
        StartCode();
    }

    private void StartCode()
    {
        fader = GameObject.FindGameObjectWithTag("fader").GetComponent<Fader>();
    }

    public void WhatButton()
    {
        if(whatButton == "Button 1")
        {
            fader.whatButton = 1;
            fader.LoadLevel();
        }

        if (whatButton == "Button 2")
        {
            fader.whatButton = 2;
            fader.LoadLevel();
        }

        if (whatButton == "Button 3")
        {
            fader.whatButton = 3;
            fader.LoadLevel();
        }

        if (whatButton == "Button 4")
        {
            fader.whatButton = 4;
            fader.LoadLevel();
        }

        if (whatButton == "Button 5")
        {
            fader.whatButton = 5;
            fader.LoadLevel();
        }

        if (whatButton == "Button 6")
        {
            fader.whatButton = 6;
            fader.LoadLevel();
        }

        if (whatButton == "Button 7")
        {
            fader.whatButton = 7;
            fader.LoadLevel();
        }

        if (whatButton == "Button 8")
        {
            fader.whatButton = 8;
            fader.LoadLevel();
        }

        if (whatButton == "Button 9")
        {
            fader.whatButton = 9;
            fader.LoadLevel();
        }

        if (whatButton == "Button 10")
        {
            fader.whatButton = 10;
            fader.LoadLevel();
        }

        if (whatButton == "Button 11")
        {
            fader.whatButton = 11;
            fader.LoadLevel();
        }

        if (whatButton == "Button 12")
        {
            fader.whatButton = 12;
            fader.LoadLevel();
        }

        if (whatButton == "Button 13")
        {
            fader.whatButton = 13;
            fader.LoadLevel();
        }

        if (whatButton == "Button 14")
        {
            fader.whatButton = 14;
            fader.LoadLevel();
        }

        if (whatButton == "Button 15")
        {
            fader.whatButton = 15;
            fader.LoadLevel();
        }
    }

    public void GetScene()
    {
        if(whatButton == "Grass Theme")
        {
            fader.gameNumber = SceneNumber;
            fader.GreenSelectionScene();
        }

        if (whatButton == "StoneTheme")
        {
            fader.gameNumber = SceneNumber;
            fader.GreenSelectionScene();
        }

        if (whatButton == "SpaceTheme")
        {
            fader.gameNumber = SceneNumber;
            fader.GreenSelectionScene();
        }


        if (whatButton == "MenyButton")
        {
            fader.gameNumber = SceneNumber;
            fader.GreenSelectionScene();
        }

        if(whatButton == "ExitGame")
        {
            fader.ExitGame();
        }
    }
}
