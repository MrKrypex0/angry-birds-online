using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] CameraMovement camMovement;
    [SerializeField] Canvas canvas;
    [SerializeField] AudioSource hitsound;
    public ParticleSystem particels;
    public ParticleSystem particels2;
    public GameObject blackBackgroundImage;
    [SerializeField] Animator anim;

    [Header("Values")]
    public string whatObject;


    public void Awake()
    {
        StartCode();
    }

    private void StartCode()
    {
        if (whatObject == "SelectionScreen")
        {
            camMovement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
            hitsound = GameObject.FindGameObjectWithTag("HitAudio").GetComponent<AudioSource>();
            canvas = GetComponent<Canvas>();
            blackBackgroundImage = null;
            canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            canvas.sortingLayerName = "ForeGroundLayer";
            particels.Stop();
        }

        if (whatObject == "SpaceTheme")
        {
            camMovement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
            hitsound = GameObject.FindGameObjectWithTag("HitAudio").GetComponent<AudioSource>();
            particels.Stop();
        }

        if (whatObject == "Button")
        {
            camMovement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
            hitsound = GameObject.FindGameObjectWithTag("HitAudio").GetComponent<AudioSource>();
            particels.Stop();
            particels2.Stop();
        }

        if (whatObject == "GrassTheme")
        {
            camMovement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
            hitsound = GameObject.FindGameObjectWithTag("HitAudio").GetComponent<AudioSource>();
            particels.Stop();
        }

        if (whatObject == "StoneTheme")
        {
            camMovement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
            hitsound = GameObject.FindGameObjectWithTag("HitAudio").GetComponent<AudioSource>();
            particels.Stop();
        }

        if (whatObject == "MenyButton")
        {
            camMovement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
            hitsound = GameObject.FindGameObjectWithTag("HitAudio").GetComponent<AudioSource>();
            particels.Stop();
        }
   
    }

    public void Event()
    {
        if(whatObject == "SelectionScreen")
        {
            camMovement.shootShake = true;
            hitsound.Play();
            particels.Play();
        }
    }

    public void ButtonEvent()
    {
        if (whatObject == "Button")
        {
            camMovement.shootShake = true;
            hitsound.Play();
            particels.Play();
        }

        if (whatObject == "GrassTheme")
        {
            camMovement.shootShake = true;
            hitsound.Play();
            particels.Play();
        }

        if (whatObject == "StoneTheme")
        {
            camMovement.shootShake = true;
            hitsound.Play();
            particels.Play();
        }

        if (whatObject == "SpaceTheme")
        {
            camMovement.shootShake = true;
            hitsound.Play();
            particels.Play();
        }

        if (whatObject == "MenyButton")
        {
            camMovement.shootShake = true;
            hitsound.Play();
            particels.Play();
        }
    }

    public void EneableGlowingParticels()
    {
        if(whatObject == "Button")
        {
            particels2.Play();
        }
    }

    public void DisableThisObject()
    {
        if(whatObject == "SelectionScreen")
        {
            gameObject.SetActive(false);
        }

        if(whatObject == "InGameCanvas")
        {
            GameObject Image = GameObject.FindGameObjectWithTag("BlackImage");
            Image.SetActive(false);
        }

    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteKey("HasPlayed");
        PlayerPrefs.DeleteKey("Highscore");
        PlayerPrefs.Save();
    }

}
