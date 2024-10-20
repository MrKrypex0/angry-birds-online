using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SpecialPowerScript : MonoBehaviour
{
    [Header("General Components")]
    public string whatAbility;
    public bool abilityActivated;
    [SerializeField] CameraMovement cam;
    [SerializeField] Camera cams;
    [SerializeField] PostProcessVolume postFX;
    [SerializeField] MultiplayerProjectile projectile;
    [SerializeField] ParticleSystem animeSpeedLines;
    [SerializeField] AudioSource audioSource;
    [SerializeField] EndMenyScript endScript;
    [SerializeField] bool alreadyUsed;

    [Header("SpeedCharacter Components")]
    public Rigidbody2D rb;
    public int bonusSpeed;

    [Header("MagicCharacter Components")]
    public GameObject magicPoint1;
    public GameObject magicPoint2;
    public GameObject magicWall;
    public GameObject negativMagicWall;

    [Header("SpikeCharacter Components")]
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;
    public GameObject spikeGameObject;
    public int amountOfSpikes;

    [Header("TreeCharacter and LavaCharacters Components")]
    public GameObject abilityPoint1;
    public GameObject abilityPoint2;
    public GameObject abilityPoint3;
    public GameObject treeComet;

    bool startCamZoom = false;
    Vector3 cameraStartOffset;
    float cameraZoom;
    float camStartSize;
    float shadeSize;
    float startShadeSize;

    [Header("BombCharacter Components")]
    public GameObject explossion;

    //private void Start()
    //{
    //    endScript = GameObject.FindGameObjectWithTag("endscreen").GetComponent<EndMenyScript>();
    //}

    public void GetComponents()
    {
        if(gameObject.activeSelf == true)
        {
            rb = null;
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
            cams = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            animeSpeedLines = GameObject.FindGameObjectWithTag("chargeeffect").GetComponent<ParticleSystem>();
            postFX = GameObject.FindGameObjectWithTag("postfx").GetComponent<PostProcessVolume>();
            audioSource = GetComponent<AudioSource>();
            projectile = GetComponent<MultiplayerProjectile>();
            camStartSize = cams.fieldOfView;
            cameraZoom = camStartSize;
            Vignette shade;
            postFX.profile.TryGetSettings<Vignette>(out shade);
            startShadeSize = shade.intensity.value;
            shadeSize = startShadeSize;
            cameraStartOffset = cam.startOffset;

            if(whatAbility == "speedability")
            {
                rb = GetComponent<Rigidbody2D>();
            }

            if(whatAbility == "magicability")
            {
                abilityPoint1 = null;
                abilityPoint2 = null;
                abilityPoint3 = null;
                point1 = null;
                point2 = null;
                point3 = null;
                point4 = null;
            }

            if(whatAbility == "spikeability")
            {
                abilityPoint1 = null;
                abilityPoint2 = null;
                abilityPoint3 = null;
            }

            if(whatAbility == "explossion")
            {
                abilityPoint1 = null;
                abilityPoint2 = null;
                abilityPoint3 = null;
                point1 = null;
                point2 = null;
                point3 = null;
                point4 = null;
            }
        }
    }

    private void Update()
    {
        GetComponents();
        BeforeAbility();

        if(projectile.released == true)
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                if(endScript.displayTexture.activeInHierarchy == false)
                {
                    ActivateAbility();
                }
            }
        }
    }

    private void BeforeAbility()
    {
        if (startCamZoom == true)
        {
            cameraZoom -= Time.deltaTime * 50;
            cams.fieldOfView = cameraZoom;

            

            Vignette shade;
            postFX.profile.TryGetSettings<Vignette>(out shade);

            shadeSize += Time.deltaTime / 4.25f;
            shade.intensity.value = shadeSize;
        }
        else
        {
            Vignette shade;
            postFX.profile.TryGetSettings<Vignette>(out shade);
            shade.intensity.value = 0.37f;
        }
    }

    private void ActivateAbility()
    {  
        StartCoroutine(BeforeAbilityEvent()); 
    }

    IEnumerator BeforeAbilityEvent()
    {
        abilityActivated = true;
        audioSource.Play();
        cam.targets.Remove(cam.targets[0]);
        cam.targets.Add(gameObject.transform);

        if(projectile.hasHitGround == true)
        {
            cam.offset = new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z);
        }
        else
        {
            cam.offset = new Vector3(cam.transform.position.x, transform.position.y - 3, cam.transform.position.z);
        }

        startCamZoom = true;
        cam.zoomshake = true;
        animeSpeedLines.Play();

        if(projectile.released == true)
        {
            Time.timeScale = 0.5f;
            projectile.rb.velocity = transform.right * 0;
            projectile.rb.gravityScale = 0;
        }

        yield return new WaitForSeconds(1);
        audioSource.Stop();
        animeSpeedLines.Stop();
        Time.timeScale = 1;
        startCamZoom = false;
        cameraZoom = 0;

        WhatCharacterAbility();
    }

    private void WhatCharacterAbility()
    {
        if(alreadyUsed == false)
        {
            if (whatAbility == "treeability")
            {

                CometAbility();

            }

            if (whatAbility == "lavaability")
            {

                CometAbility();

            }

            if (whatAbility == "speedability")
            {
                SpeedAbility();
            }

            if (whatAbility == "magicability")
            {

                MagicAbility();

            }

            if (whatAbility == "spikeability")
            {
                SpikeAbility();
            }

            if (whatAbility == "explossion")
            {
                if (projectile.hasHitGround == true)
                {
                    ExplossionAbility();
                }
            }
        }
        
    }

    public void ResetCameraZoom()
    {
        abilityActivated = false;
        Vignette shade;
        postFX.profile.TryGetSettings<Vignette>(out shade);
        shade.intensity.value = startShadeSize;
        cams.fieldOfView = camStartSize;
        cameraZoom = camStartSize;
        animeSpeedLines.Clear();
        animeSpeedLines.Stop();
        cam.targets.Remove(cam.targets[0]);
        cam.targets.Add(GameObject.FindGameObjectWithTag("focus").transform);
        cam.offset = cameraStartOffset;
    }

    private void CometAbility()
    {
        ResetCameraZoom();
        projectile.rb.gravityScale = 1;
        Instantiate(treeComet, abilityPoint1.transform.position, abilityPoint1.transform.rotation);
        Instantiate(treeComet, abilityPoint2.transform.position, abilityPoint2.transform.rotation);
        Instantiate(treeComet, abilityPoint3.transform.position, abilityPoint3.transform.rotation);
        alreadyUsed = true;
    }

    private void SpeedAbility()
    {
        ResetCameraZoom();
        projectile.rb.gravityScale = 1;
        rb.AddForce(transform.right * bonusSpeed, ForceMode2D.Impulse);
        alreadyUsed = true;
    }

    private void MagicAbility()
    {
        ResetCameraZoom();
        projectile.rb.gravityScale = 1;
        Instantiate(magicWall, magicPoint1.transform.position, magicPoint1.transform.rotation);
        Instantiate(negativMagicWall, magicPoint2.transform.position, magicPoint2.transform.rotation);
        alreadyUsed = true;
    }

    private void SpikeAbility()
    {
        ResetCameraZoom();
        projectile.rb.gravityScale = 1;
        for (int i = 0; i < amountOfSpikes; i++)
        {
            Instantiate(spikeGameObject, point1.transform);
            Instantiate(spikeGameObject, point2.transform);
            Instantiate(spikeGameObject, point3.transform);
            Instantiate(spikeGameObject, point4.transform);
            alreadyUsed = true;
        }
    }

    private void ExplossionAbility()
    {
        projectile.rb.gravityScale = 1;
        Instantiate(explossion, gameObject.transform);
        alreadyUsed = true;
    }
}
