using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using Mirror;
using System.Security.AccessControl;
using DapperDino.Mirror.Tutorials.Lobby;
using System;

public enum itemType
{
    woodPillar,
    stonePillar,
    longStonePillar,
    longwoodPillar,
    stonePillar2,
    stonePillar3,
    stonePillar4,
    staticMoon,
    treebox,
    empty,
}

public class Inventory : NetworkBehaviour
{
    public int wichPlayer;
    public itemType item;
    public Slider slider;
    public Vector2 pinPoint;
    [SerializeField]
    public List<GameObject> itemList;
    [SerializeField] private NetworkGamePlayerLobby gamePlayer;
    [SyncVar]
    [SerializeField] bool spawnFromClient = false;
    [SyncVar]
    [SerializeField] Vector2 spawnPosFromClient;
    [SyncVar]
    [SerializeField] bool oneTime = false;

    [SerializeField] private NetworkTransform netWorkTransfrom;

    private Controls controls;

    private Controls Controls
    {
        get
        {
            if(controls != null) { return controls; }

            return controls = new Controls();
        }
    }

    [ClientCallback]
    private void OnEnable() => Controls.Enable();

    [ClientCallback]
    private void OnDisable() => Controls.Disable();

    private void Start()
    {
        item = itemType.empty;
        gamePlayer = GetComponent<NetworkGamePlayerLobby>();
        if(gamePlayer.isLeader == false)
        {
            if(wichPlayer == 1)
            {
                
            }

            if(wichPlayer == 2)
            {
                netWorkTransfrom.clientAuthority = true;
            }
        }
    }

    private Vector3 MouseClick(float z)
    {
        Vector2 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    private void Update()
    {
        if (slider == null)
        {
            slider = GameObject.FindGameObjectWithTag("slider").GetComponent<Slider>();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                SetItemOnPinPoint();

                if (gamePlayer.isLeader == false)
                {
                    ClientOnPinPoint();
                }
            }
        }
    }

    private void ClientOnPinPoint()
    {
        if(wichPlayer == 2)
        {
            if(gamePlayer.isLeader == false)
            {
                if (isClientOnly == true)
                {
                    spawnPosFromClient = MouseClick(0);
                    oneTime = false;
                    ClientSetItem(spawnPosFromClient, slider.value);
                }
            }
        }
    }

    public void SetItemOnPinPoint()
    {
        if(wichPlayer == 1)
        {
            if(gamePlayer.isLeader == true)
            {
                if (isServer)
                {
                    Vector2 MousePos = MouseClick(0);
                    print("Sever");
                    RpcServerSetItem(MousePos);
                }
            }
        }
    }

    [Client]
    private void ClientSetItem(Vector2 mousePosition, float sliderValue)
    {
        if (item == itemType.woodPillar)
        {
            CmdWoodPillar(mousePosition, sliderValue);
        }

        if (item == itemType.stonePillar)
        {
            CmdStonePillar(mousePosition, sliderValue);
        }

        if (item == itemType.longStonePillar)
        {
            CmdLongStonePillar(mousePosition, sliderValue);
        }

        if (item == itemType.longwoodPillar)
        {
            CmdLongWoodPillar(mousePosition, sliderValue);
        }

        if (item == itemType.stonePillar4)
        {
            CmdStonePillar4(mousePosition, sliderValue);
        }

        if (item == itemType.stonePillar2)
        {
            CmdStonePillar2(mousePosition, sliderValue);
        }

        if (item == itemType.stonePillar3)
        {
            CmdStonePillar3(mousePosition, sliderValue);
        }

        if (item == itemType.treebox)
        {
            CmdTreebox(mousePosition, sliderValue);
        }

        if (item == itemType.empty)
        {
            print("empty");
        }
        oneTime = true;
    }

    [Command(ignoreAuthority = true)]
    private void CmdTreebox(Vector2 mousePosition, float sliderValue)
    {
        GameObject gameobject = Instantiate(itemList[8].gameObject, mousePosition, Quaternion.Euler(0, 0, sliderValue));
        GameObject owner = gameObject;
        NetworkServer.Spawn(gameobject, owner);
    }

    [Command(ignoreAuthority = true)]
    private void CmdStonePillar3(Vector2 mousePosition, float sliderValue)
    {
        GameObject gameobject = Instantiate(itemList[6].gameObject, mousePosition, Quaternion.Euler(0, 0, sliderValue));
        GameObject owner = gameObject;
        NetworkServer.Spawn(gameobject, owner);
    }

    [Command(ignoreAuthority = true)]
    private void CmdStonePillar2(Vector2 mousePosition, float sliderValue)
    {
        GameObject gameobject = Instantiate(itemList[5].gameObject, mousePosition, Quaternion.Euler(0, 0, sliderValue));
        GameObject owner = gameObject;
        NetworkServer.Spawn(gameobject, owner);
    }

    [Command(ignoreAuthority = true)]
    private void CmdStonePillar4(Vector2 mousePosition, float sliderValue)
    {
        GameObject gameobject = Instantiate(itemList[4].gameObject, mousePosition, Quaternion.Euler(0, 0, sliderValue));
        GameObject owner = gameObject;
        NetworkServer.Spawn(gameobject, owner);
    }

    [Command(ignoreAuthority = true)]
    private void CmdLongWoodPillar(Vector2 mousePosition, float sliderValue)
    {
        GameObject gameobject = Instantiate(itemList[3].gameObject, mousePosition, Quaternion.Euler(0, 0, sliderValue));
        GameObject owner = gameObject;
        NetworkServer.Spawn(gameobject, owner);
    }

    [Command(ignoreAuthority = true)]
    private void CmdLongStonePillar(Vector2 mousePosition, float sliderValue)
    {
        GameObject gameobject = Instantiate(itemList[2].gameObject, mousePosition, Quaternion.Euler(0, 0, sliderValue));
        GameObject owner = gameObject;
        NetworkServer.Spawn(gameobject, owner);
    }

    [Command(ignoreAuthority = true)]
    private void CmdStonePillar(Vector2 mousePosition, float sliderValue)
    {
        GameObject gameobject = Instantiate(itemList[1].gameObject, mousePosition, Quaternion.Euler(0, 0, sliderValue));
        GameObject owner = gameObject;
        NetworkServer.Spawn(gameobject, owner);
    }

    [Command(ignoreAuthority = true)]
    private void CmdWoodPillar(Vector2 mousePosition, float sliderValue)
    {
        GameObject gameobject = Instantiate(itemList[0].gameObject, mousePosition, Quaternion.Euler(0, 0, sliderValue));
        GameObject owner = gameObject;
        NetworkServer.Spawn(gameobject, owner);
    }

    [ClientRpc]
    private void RpcServerSetItem(Vector2 mousePosition)
    {
        if (item == itemType.woodPillar)
        {
            GameObject gameobject = Instantiate(itemList[0].gameObject, mousePosition, Quaternion.Euler(0, 0, slider.value));
            GameObject owner = gameObject;
            NetworkServer.Spawn(gameobject, owner);
        }

        if (item == itemType.stonePillar)
        {
            GameObject gameobject = Instantiate(itemList[1].gameObject, mousePosition, Quaternion.Euler(0, 0, slider.value));
            GameObject owner = gameObject;
            NetworkServer.Spawn(gameobject, owner);
        }

        if (item == itemType.longStonePillar)
        {
            GameObject gameobject = Instantiate(itemList[2].gameObject, mousePosition, Quaternion.Euler(0, 0, slider.value));
            GameObject owner = gameObject;
            NetworkServer.Spawn(gameobject, owner);
        }

        if (item == itemType.longwoodPillar)
        {
            GameObject gameobject = Instantiate(itemList[3].gameObject, mousePosition, Quaternion.Euler(0, 0, slider.value));
            GameObject owner = gameObject;
            NetworkServer.Spawn(gameobject, owner);
        }

        if (item == itemType.stonePillar4)
        {
            GameObject gameobject = Instantiate(itemList[4].gameObject, mousePosition, Quaternion.Euler(0, 0, slider.value));
            GameObject owner = gameObject;
            NetworkServer.Spawn(gameobject, owner);
        }

        if (item == itemType.stonePillar2)
        {
            GameObject gameobject = Instantiate(itemList[5].gameObject, mousePosition, Quaternion.Euler(0, 0, slider.value));
            GameObject owner = gameObject;
            NetworkServer.Spawn(gameobject, owner);
        }

        if (item == itemType.stonePillar3)
        {
            GameObject gameobject = Instantiate(itemList[6].gameObject, mousePosition, Quaternion.Euler(0, 0, slider.value));
            GameObject owner = gameObject;
            NetworkServer.Spawn(gameobject, owner);
        }

        if (item == itemType.staticMoon)
        {
            GameObject gameobject = Instantiate(itemList[7].gameObject, mousePosition, Quaternion.Euler(0, 0, slider.value));
            GameObject owner = gameObject;
            NetworkServer.Spawn(gameobject, owner);
        }

        if (item == itemType.treebox)
        {
            GameObject gameobject = Instantiate(itemList[8].gameObject, mousePosition, Quaternion.Euler(0, 0, slider.value));
            GameObject owner = gameObject;
            NetworkServer.Spawn(gameobject, owner);
        }

        if (item == itemType.empty)
        {
            print("empty");
        }
    }
}
