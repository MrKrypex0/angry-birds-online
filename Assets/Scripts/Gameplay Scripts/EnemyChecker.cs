using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> players;
    [SerializeField] EndMenyScript endMenyScript;
    [SerializeField] AddAmmo addAmo;

    private void Start()
    {
        addAmo = GameObject.FindGameObjectWithTag("AMO").GetComponent<AddAmmo>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("enemy"));
        endMenyScript = GameObject.FindGameObjectWithTag("endscreen").GetComponent<EndMenyScript>();
    }

    private void Update()
    {
        players = addAmo.objects;
        ListUpdateFunction();
        NoEnemiesLeftFunction();
    }

    private void ListUpdateFunction()
    {
        foreach (GameObject objects in enemies)
        {
            enemies.RemoveAll(target => target == null);
        }
    }

    public void NoEnemiesLeftFunction()
    {
        if(enemies.Count == 0)
        {
            endMenyScript.EnableTextures();
        }

        if(addAmo.canvas.activeInHierarchy == false)
        {
            if (players.Count == 0)
            {
                endMenyScript.EnableTextures();
            }
        }
    }
}
