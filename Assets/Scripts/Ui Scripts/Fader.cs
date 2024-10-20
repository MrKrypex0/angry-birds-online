using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    public Animator anim;

    public string whatScene;

    public int whatButton;

    public int gameNumber;

    public float transitionTime = 1;

    public void LoadLevel()
    {
        if(whatScene == "Selection Scene")
        {
            
             gameNumber = whatButton;
             StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + gameNumber));       
        }
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        anim.SetBool("transition", true);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void GreenSelectionScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + gameNumber)); 
    }

    public void ExitGame()
    {
        print("ExitGame");
        Application.Quit();
    }
}
