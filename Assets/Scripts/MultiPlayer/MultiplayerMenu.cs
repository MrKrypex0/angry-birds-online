using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerMenu : MonoBehaviour
{
    public Fader fader;
    public int sceneNumber;

    public bool player1Ready;
    public bool player2Ready;

    public void StartButton()
    {
        if(player1Ready == true && player2Ready == true)
        {
            fader.whatButton = sceneNumber;
            fader.LoadLevel();
        }
    }

    public void Player1Ready()
    {
        player1Ready = true;
    }

    public void Player2Ready()
    {
        player2Ready = true;
    }
}
