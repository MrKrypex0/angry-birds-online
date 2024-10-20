using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageDisplay : MonoBehaviour
{
    [Header("Values")]
    public bool slotIsNotEmpty = false;
    public int index;

    [Header("Components")]
    public Image astroPlayer;
    public Image treePlayer;
    public Image lavaPlayer;
    public Image spikePlayer;
    public Image darkPlayer;
    public Image speedPlayer;
    public Image bombPlayer;
    public String slot;
    public bool isNotEmpty;
    public string buttonType;
    [SerializeField] AddAmmo amo;

    void Start()
    {
        StartCode();
    }

    void Update()
    {
        WhatIsTheIndex();
    }

    public void StartCode()
    {
        amo = GameObject.FindGameObjectWithTag("AMO").GetComponent<AddAmmo>();
        astroPlayer.enabled = false;
        treePlayer.enabled = false;
        lavaPlayer.enabled = false;
        spikePlayer.enabled = false;
        darkPlayer.enabled = false;
        speedPlayer.enabled = false;
        bombPlayer.enabled = false;
    }
    private void CheckWichPlayer()
    {
        if (buttonType == "astroplayer")
        {
            astroPlayer.enabled = true;
            slotIsNotEmpty = true;
        }

        if (buttonType == "treeplayer")
        {
            treePlayer.enabled = true;
            slotIsNotEmpty = true;
        }

        if (buttonType == "lavaplayer")
        {
            lavaPlayer.enabled = true;
            slotIsNotEmpty = true;
        }

        if (buttonType == "spikeplayer")
        {
            spikePlayer.enabled = true;
            slotIsNotEmpty = true;
        }

        if (buttonType == "darkplayer")
        {
            darkPlayer.enabled = true;
            slotIsNotEmpty = true;
        }
        
        if (buttonType == "speedplayer")
        {
            speedPlayer.enabled = true;
            slotIsNotEmpty = true;
        }

        if (buttonType == "bombplayer")
        {
            bombPlayer.enabled = true;
            slotIsNotEmpty = true;
        }
    }
    public void WhatIsTheIndex()
    {
        Slot1();
        Slot2();
        Slot3();
        Slot4();
        Slot5();

    }
    private void Slot1()
    {
        if (slotIsNotEmpty == false)
        {
            if (index == 1)
            {
                if (index == amo.index)
                {
                    if (slot == "Slot1")
                    {
                        CheckWichPlayer();
                    }
                }
            }
        }
    }
    private void Slot2()
    {
        if (slotIsNotEmpty == false)
        {
            if (index == 2)
            {
                if (index == amo.index)
                {
                    CheckWichPlayer();
                }
            }
        }
    }
    private void Slot3()
    {
        if (slotIsNotEmpty == false)
        {
            if (index == 3)
            {
                if (index == amo.index)
                {
                    if (slot == "Slot3")
                    {
                        CheckWichPlayer();
                    }
                }
            }
        }
    }
    private void Slot4()
    {
        if (slotIsNotEmpty == false)
        {
            if (index == 4)
            {
                if (index == amo.index)
                {
                    if (slot == "Slot4")
                    {
                        CheckWichPlayer();
                    }
                }
            }
        }
    }
    private void Slot5()
    {
        if (slotIsNotEmpty == false)
        {
            if (index == 5)
            {
                if (index == amo.index)
                {
                    if (slot == "Slot5")
                    {
                        CheckWichPlayer();
                    }
                }
            }
        }
    }
}
