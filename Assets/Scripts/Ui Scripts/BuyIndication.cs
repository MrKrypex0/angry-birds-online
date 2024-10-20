using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyIndication : MonoBehaviour
{
    public string ownedPlayer;
    public TextMeshProUGUI moneyText;
    public int buyAmount;

    [SerializeField] Animator anim;

    public Image astroplayer;
    public Image treeplayer;
    public Image lavaplayer;
    public Image spikeplayer;
    public Image darkplayer;
    public Image speedplayer;
    public Image bombplayer;

    private void Start()
    {
        StartCode();
    }

    private void Update()
    {
        SetcharacterActive();
    }
    private void StartCode()
    {
        anim = GetComponent<Animator>();
        astroplayer.enabled = false;
        treeplayer.enabled = false;
        lavaplayer.enabled = false;
        spikeplayer.enabled = false;
        darkplayer.enabled = false;
        speedplayer.enabled = false;
        bombplayer.enabled = false;
    }

    private void SetcharacterActive()
    {
        if (ownedPlayer == "astroplayer")
        {
            astroplayer.enabled = true;
            treeplayer.enabled = false;
            lavaplayer.enabled = false;
            spikeplayer.enabled = false;
            darkplayer.enabled = false;
            speedplayer.enabled = false;
            bombplayer.enabled = false;
            StartAnimation();
        }

        if (ownedPlayer == "treeplayer")
        {
            astroplayer.enabled = false;
            treeplayer.enabled = true;
            lavaplayer.enabled = false;
            spikeplayer.enabled = false;
            darkplayer.enabled = false;
            speedplayer.enabled = false;
            bombplayer.enabled = false;
            StartAnimation();
        }

        if (ownedPlayer == "lavaplayer")
        {
            astroplayer.enabled = false;
            treeplayer.enabled = false;
            lavaplayer.enabled = true;
            spikeplayer.enabled = false;
            darkplayer.enabled = false;
            speedplayer.enabled = false;
            bombplayer.enabled = false;
            StartAnimation();
        }

        if (ownedPlayer == "spikeplayer")
        {
            astroplayer.enabled = false;
            treeplayer.enabled = false;
            lavaplayer.enabled = false;
            spikeplayer.enabled = true;
            darkplayer.enabled = false;
            speedplayer.enabled = false;
            bombplayer.enabled = false;
            StartAnimation();
        }

        if (ownedPlayer == "darkplayer")
        {
            astroplayer.enabled = false;
            treeplayer.enabled = false;
            lavaplayer.enabled = false;
            spikeplayer.enabled = false;
            darkplayer.enabled = true;
            speedplayer.enabled = false;
            bombplayer.enabled = false;
            StartAnimation();
        }

        if (ownedPlayer == "speedplayer")
        {
            astroplayer.enabled = false;
            treeplayer.enabled = false;
            lavaplayer.enabled = false;
            spikeplayer.enabled = false;
            darkplayer.enabled = false;
            speedplayer.enabled = true;
            bombplayer.enabled = false;
            StartAnimation();
        }

        if (ownedPlayer == "bombplayer")
        {
            astroplayer.enabled = false;
            treeplayer.enabled = false;
            lavaplayer.enabled = false;
            spikeplayer.enabled = false;
            darkplayer.enabled = false;
            speedplayer.enabled = false;
            bombplayer.enabled = true;
            StartAnimation();
        }
    }

    private void StartAnimation()
    {
        moneyText.text = buyAmount.ToString();
        anim.SetTrigger("dropdown");
        anim.SetTrigger("smallermoney");
    }

    //Called by a AnimationEvent
    private void ResetString()
    {
        ownedPlayer = null;
    }
}
