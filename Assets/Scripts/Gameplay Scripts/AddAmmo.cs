using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddAmmo : MonoBehaviour
{
    [Header("Ints")]
    public int astroPlayerIndex;
    public int treePlayerIndex;
    public int lavaPlayerIndex;
    public int spikePlayerIndex;
    public int darkPlayerIndex;
    public int speedPlayerIndex;
    public int bombPlayerIndex;
    public int index;

    [Header("Components")]
    public List<GameObject> characters;
    public List<GameObject> objects;
    public GameObject astroPlayerObject;
    public GameObject treePlayerObject;
    public GameObject lavaPlayerObject;
    public GameObject spikePlayerObject;
    public GameObject darkPlayerObject;
    public GameObject speedPlayerObject;
    public GameObject bombPlayerObject;
    public GameObject currentObject;
    public GameObject canvas;
    [SerializeField] private EndMenyScript endMenyScript;

    private void Awake()
    {
        endMenyScript = GameObject.FindGameObjectWithTag("endscreen").GetComponent<EndMenyScript>();
    }

    private void Start()
    {
        StartCode();
    }

    private void Update()
    {
        UpDateList();
        CheckIfPlayerIsAlive();
        index = treePlayerIndex + astroPlayerIndex + lavaPlayerIndex + spikePlayerIndex + darkPlayerIndex + speedPlayerIndex + bombPlayerIndex;
    }

    private void StartCode()
    {
        canvas = GameObject.FindGameObjectWithTag("SelectionCanvas");
    }

    private void CheckIfPlayerIsAlive()
    {
        if (canvas.activeInHierarchy == false)
        {

            if (currentObject == null)
            {
                objects[0].SetActive(true);
                currentObject = GameObject.FindGameObjectWithTag("Character1");
            }
        }

    }

    private void UpDateList()
    {
        characters.RemoveAll(target => target == null);
        objects.RemoveAll(target => target == null);
    }

    public void AddCharacters()
    {
        for (int i = 0; i < astroPlayerIndex; i++)
        {
            characters.Add(astroPlayerObject);
        }

        for(int i = 0; i < treePlayerIndex; i++)
        {
            characters.Add(treePlayerObject);
        }

        for (int i = 0; i < lavaPlayerIndex; i++)
        {
            characters.Add(lavaPlayerObject);
        }

        for (int i = 0; i < spikePlayerIndex; i++)
        {
            characters.Add(spikePlayerObject);
        }

        for (int i = 0; i < darkPlayerIndex; i++)
        {
            characters.Add(darkPlayerObject);
        }

        for (int i = 0; i < speedPlayerIndex; i++)
        {
            characters.Add(speedPlayerObject);
        }

        for (int i = 0; i < bombPlayerIndex; i++)
        {
            characters.Add(bombPlayerObject);
        }
    }

    public void GetCharacter()
    {
        objects.AddRange(GameObject.FindGameObjectsWithTag("Character1"));
        foreach(GameObject target in objects)
        {
            target.SetActive(false);
        }

        objects[1].SetActive(true);
        currentObject = GameObject.FindGameObjectWithTag("Character1");

    }

    public void InstantiateAllCharacters()
    {
        foreach (GameObject target in characters)
        {
            Instantiate(target, transform.position, transform.rotation);
        }
        GetCharacter();
        astroPlayerIndex = 0;
        treePlayerIndex = 0;
        lavaPlayerIndex = 0;
        spikePlayerIndex = 0;
        darkPlayerIndex = 0;
        spikePlayerIndex = 0;
        bombPlayerIndex = 0;
    }
}
