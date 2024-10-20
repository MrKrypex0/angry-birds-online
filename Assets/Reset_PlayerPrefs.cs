using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_PlayerPrefs : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("SavedHighscore", 0);
        PlayerPrefs.SetInt("BombPlayer", 0);
        PlayerPrefs.SetInt("DarkPlayer", 0);
        PlayerPrefs.SetInt("LavaPlayer", 0);
        PlayerPrefs.SetInt("SpeedPlayer", 0);
        PlayerPrefs.SetInt("SpikePlayer", 0);
        PlayerPrefs.SetInt("TreePlayer", 0);

        Debug.Log(PlayerPrefs.GetInt("SavedHighscore"));
        Debug.Log(PlayerPrefs.GetInt("BombPlayer"));
        Debug.Log(PlayerPrefs.GetInt("DarkPlayer"));
        Debug.Log(PlayerPrefs.GetInt("LavaPlayer"));
        Debug.Log(PlayerPrefs.GetInt("SpeedPlayer"));
        Debug.Log(PlayerPrefs.GetInt("SpikePlayer"));
        Debug.Log(PlayerPrefs.GetInt("TreePlayer"));
    }

}
