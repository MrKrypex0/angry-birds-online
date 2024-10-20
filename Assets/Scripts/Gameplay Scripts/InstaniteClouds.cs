using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaniteClouds : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float timeLeft = 3;
    [SerializeField] bool startTimer = false;
    [SerializeField] int Pointindex;
    [SerializeField] int cloudIndex;

    [Header("Components")]
    public GameObject[] Clouds;
    public List<GameObject> bigClouds;
    public List<GameObject> smallClouds;
    public Transform[] InstanitePoints;
    [SerializeField] GameObject currentCloudObject;
    [SerializeField] Transform currentPointObject;

    void Start()
    {
        SpawnFunction();
    }

    void Update()
    {
        UpdateClouds();
    }

    private void UpdateClouds()
    {
        if(startTimer == true)
        {
            timeLeft -= Time.deltaTime;

            if(timeLeft < 0)
            {
                SpawnFunction();
                timeLeft = 3;
            }
        }
    }

    void SpawnFunction()
    {
        cloudIndex = UnityEngine.Random.Range(0, Clouds.Length);
        currentCloudObject = Clouds[cloudIndex];

        Pointindex = UnityEngine.Random.Range(0, InstanitePoints.Length);
        currentPointObject = InstanitePoints[Pointindex];

        Instantiate(currentCloudObject, currentPointObject);

        bigClouds.Add(GameObject.FindGameObjectWithTag("bigcloud"));
        smallClouds.Add(GameObject.FindGameObjectWithTag("smallcloud"));

        currentCloudObject = null;
        currentPointObject = null;

        bigClouds.RemoveAll(item => item == null);
        smallClouds.RemoveAll(item => item == null);

        startTimer = true;

    }
}
