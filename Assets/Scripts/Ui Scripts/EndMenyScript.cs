using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndMenyScript : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] int maxScore;
    [SerializeField] int score;
    public int sceneNumber;
    public int pillarPoints;
    public int enemyPoints;
    public int boxPoints;

    [Header("Components")]
    public GameObject starPrefab;
    public GameObject displayTexture;
    public GameObject instantiatePoint1;
    public GameObject instantiatePoint2;
    public GameObject instantiatePoint3;
    [SerializeField] List<GameObject> pillarList;
    [SerializeField] List<GameObject> enemyList;
    [SerializeField] List<GameObject> boxList;
    public Animator anim;
    public ParticleSystem particel;
    [SerializeField] AudioSource hitsound;
    [SerializeField] CameraMovement camMove;
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject selectionMeny;
    [SerializeField] Fader fader;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        GetAllObjects();
        CalculateMaxScore();
        DisableTextures();
        StartCode();
    }
    private void Update()
    {
        UpdateLists();
        FindGameObjects();
    }
    private void StartCode()
    {
        hitsound = GameObject.FindGameObjectWithTag("HitAudio").GetComponent<AudioSource>();
        camMove = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        selectionMeny = GameObject.FindGameObjectWithTag("SelectionCanvas");
        fader = GameObject.FindGameObjectWithTag("fader").GetComponent<Fader>();
        particel.Stop();
    }
    private void FindGameObjects()
    {
        if(selectionMeny.activeInHierarchy == false)
        {
            inGameCanvas = GameObject.FindGameObjectWithTag("othercanvas");
        }
    }
    private void DisableTextures()
    {
        displayTexture.SetActive(false);
    }
    public void EnableTextures()
    { 
        if(displayTexture.activeInHierarchy == false)
        {
             inGameCanvas.SetActive(false);
        }
        displayTexture.SetActive(true);
        anim.SetTrigger("EndMenyStart"); 
    }
    private void UpdateLists()
    {
        pillarList.RemoveAll(target => target == null);
        enemyList.RemoveAll(target => target == null);
        boxList.RemoveAll(target => target == null);
    }
    private void GetAllObjects()
    {
        pillarList.AddRange(GameObject.FindGameObjectsWithTag("Pillar"));
        enemyList.AddRange(GameObject.FindGameObjectsWithTag("enemy"));
        boxList.AddRange(GameObject.FindGameObjectsWithTag("box"));
    }
    private void CalculateMaxScore()
    {
        maxScore = pillarList.Count * pillarPoints + enemyList.Count * enemyPoints + boxList.Count * boxPoints;
    }
    private void CalculateScore()
    {
        score = maxScore - (pillarList.Count * pillarPoints + enemyList.Count * enemyPoints + boxList.Count * boxPoints);

        if(score >= maxScore/3)
        {
            Instantiate(starPrefab, instantiatePoint1.transform.position, instantiatePoint1.transform.rotation);
        }

        if (score >= maxScore/2)
        {
            Instantiate(starPrefab, instantiatePoint2.transform.position, instantiatePoint2.transform.rotation);
        }

        if (score == maxScore)
        {
            Instantiate(starPrefab, instantiatePoint3.transform.position, instantiatePoint3.transform.rotation);
        }
    }
    public void EndEvent()
    {
        camMove.shootShake = true;
        particel.Play();
        hitsound.Play();

        CalculateScore();
        scoreText.text = score.ToString();
    }
    public void StartSelectionMeny()
    {
        BackToGreenSelection();
    }

    private void BackToGreenSelection()
    {
        fader.gameNumber = -sceneNumber;
        fader.GreenSelectionScene(); 
    }

    public void SaveAndQuit()
    {
        PlayerPrefs.SetInt("Highscore", score);
        PlayerPrefs.SetInt("HasPlayed", 1);
        PlayerPrefs.Save();
    }
}
