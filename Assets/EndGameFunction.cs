using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class EndGameFunction : NetworkBehaviour
{
    [SerializeField] NetworkManager networkManager;
    [SerializeField] Button endButton;

    private void Start()
    {

        networkManager = GameObject.FindGameObjectWithTag("networkmanager").GetComponent<NetworkManager>();
        if (isClientOnly)
        {
            print("StopClient");
            endButton.onClick.AddListener(CmdStopclient);
        }
        else
        {
            print("StopServer");
            endButton.onClick.AddListener(StopServer);
        }
        gameObject.SetActive(false);
    }

    public void StopServer()
    {
        networkManager.StopHost();
    }

    [Command(ignoreAuthority = true)]
    public void CmdStopclient()
    {
        networkManager.StopClient();
    }
}
