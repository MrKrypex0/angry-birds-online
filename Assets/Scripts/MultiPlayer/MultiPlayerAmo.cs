using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using DapperDino.Mirror.Tutorials.Lobby;

public class MultiPlayerAmo : NetworkBehaviour
{
    [Header("Components")]
    public List<GameObject> characters;

    [SyncVar]
    public bool oneTime = false;

    [SerializeField] private RoundScript roundScript;
    [SerializeField] private NetworkGamePlayerLobby gamePlayer;

    private void Start()
    {
        if (isServer)
        {
            gamePlayer = GameObject.FindGameObjectWithTag("Player1").GetComponent<NetworkGamePlayerLobby>();
        }
    }

    //Called from another script
    [ServerCallback]
    public void RpcInstantiateCharacter()
    {
        if(gamePlayer.isLeader == true)
        {
            oneTime = false;
            if (roundScript.player1 == true)
            {
                if (oneTime == false)
                {
                    GameObject currentPlayerObject = Instantiate(characters[UnityEngine.Random.Range(0, 7)], transform.position, Quaternion.Euler(0, 180, 0));
                    NetworkServer.Spawn(currentPlayerObject);
                    oneTime = true;
                }
            }

            if (roundScript.player2 == true)
            {
                if (oneTime == false)
                {
                    GameObject currentPlayerObject = Instantiate(characters[UnityEngine.Random.Range(0, 7)], transform.position, Quaternion.Euler(0, 180, 0));
                    NetworkServer.Spawn(currentPlayerObject, connectionToClient);
                    oneTime = true;
                }
            }
        }
    }
}
