using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMeny : MonoBehaviour
{
    [SerializeField] Fader fader;
    public int sceneNumber;

    private void Start()
    {
        fader = GameObject.FindGameObjectWithTag("fader").GetComponent<Fader>();
    }

    public void SinglePlayer()
    {
        fader.gameNumber = sceneNumber;
        fader.GreenSelectionScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
