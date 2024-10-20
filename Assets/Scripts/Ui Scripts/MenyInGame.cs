using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenyInGame : MonoBehaviour
{
    [Header("Values")]
    public int wichStage;

    [Header("Components")]
    [SerializeField] GameObject selectionCanvas;
    [SerializeField] Canvas canvas;
    [SerializeField] Fader fader;
    [SerializeField] GameObject inGameMeny;
    [SerializeField] Animator anim;
    [SerializeField] GameObject blackImage;

    void Start()
    {
        StartCode();
    }

    void Update()
    {
        SelectionChecker();
    }

    private void SelectionChecker()
    {
        if (selectionCanvas == null)
        {
            canvas.enabled = true;
        }
        else if (selectionCanvas.activeInHierarchy == true)
        {
            canvas.enabled = false;
        }
        else
        {
            canvas.enabled = true;
        }
    }

    private void StartCode()
    {
        blackImage = GameObject.FindGameObjectWithTag("BlackImage");
        anim = GetComponent<Animator>();
        selectionCanvas = GameObject.FindGameObjectWithTag("SelectionCanvas");
        inGameMeny = GameObject.Find("In Game Meny");
        fader = GameObject.FindGameObjectWithTag("fader").GetComponent<Fader>();
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        inGameMeny.SetActive(false);
    }

    public void ToSelectionScene()
    {
        SetTimeScaleToMax();
        fader.gameNumber = -wichStage;
        fader.GreenSelectionScene();
    }

    public void SetInGameMenyActive()
    {
        inGameMeny.SetActive(true);
        anim.SetTrigger("InGameStart");
    }

    public void BackToThemeSelection()
    {
        PlayerPrefs.DeleteKey("Highscore");
        fader.gameNumber = -wichStage;
        fader.GreenSelectionScene();
    }

    public void SetInGameMenyNotActive()
    {
        anim.SetTrigger("InGameEnd");
    }

    public void SetTimeScaleToZero()
    {
        Time.timeScale = 0f;
    }

    public void SetTimeScaleToMax()
    {
        Time.timeScale = 1f;
    }

    public void MuteVolume()
    {
        AudioListener.volume = 0f;
    }

    public void UnMuteVolume()
    {
        AudioListener.volume = 1f;
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteKey("HasPlayed");
        PlayerPrefs.DeleteKey("Highscore");
        PlayerPrefs.Save();
    }
}
