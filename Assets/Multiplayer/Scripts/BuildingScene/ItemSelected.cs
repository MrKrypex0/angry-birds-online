using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using DapperDino.Mirror.Tutorials.Lobby;

public class ItemSelected : NetworkBehaviour
{
    [SerializeField] Inventory inventory1;
    [SerializeField] Inventory inventory2;

    [SerializeField] private Slider slider;

    [SerializeField] NetworkGamePlayerLobby gamePlayer;

    public bool gameplayer = true;

    public Image woodPillar;
    public Image stonePillar;
    public Image longStonePillar;
    public Image longwoodPillar;
    public Image treeBox;
    public Image stonePillar2;
    public Image stonePillar3;
    public Image stonePillar4;

    private void Start()
    {
        StartCode();
    }

    private void StartCode()
    {
        woodPillar.enabled = false;
        stonePillar.enabled = false;
        longStonePillar.enabled = false;
        longwoodPillar.enabled = false;
        treeBox.enabled = false;
        stonePillar2.enabled = false;
        stonePillar3.enabled = false;
        stonePillar4.enabled = false;
    }

    private void Update()
    {
        SearchForStartComponents();
        SetImageActive();
        ImageRotateUpdate();
    }

    private void SearchForStartComponents()
    {
        if (inventory1 == null)
        {
            inventory1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Inventory>();
        }

        if (inventory2 == null)
        {
            inventory2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Inventory>();
        }
    }

    private void ImageRotateUpdate()
    {
        woodPillar.gameObject.transform.rotation = Quaternion.Euler(0, 0, slider.value);
        stonePillar.gameObject.transform.rotation = Quaternion.Euler(0, 0, slider.value);
        longStonePillar.gameObject.transform.rotation = Quaternion.Euler(0, 0, slider.value);
        longwoodPillar.gameObject.transform.rotation = Quaternion.Euler(0, 0, slider.value);
        treeBox.gameObject.transform.rotation = Quaternion.Euler(0, 0, slider.value);
        stonePillar2.gameObject.transform.rotation = Quaternion.Euler(0, 0, slider.value);
        stonePillar3.gameObject.transform.rotation = Quaternion.Euler(0, 0, slider.value);
        stonePillar4.gameObject.transform.rotation = Quaternion.Euler(0, 0, slider.value);
    }

    private void SetImageActive()
    {
        if (gameplayer == true)
        {
            if (inventory1.item == itemType.woodPillar)
            {
                woodPillar.enabled = true;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }

            if (inventory1.item == itemType.stonePillar)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = true;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }

            if (inventory1.item == itemType.longwoodPillar)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = true;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }

            if (inventory1.item == itemType.staticMoon)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }

            if (inventory1.item == itemType.treebox)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = true;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }

            if (inventory1.item == itemType.longStonePillar)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = true;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }

            if (inventory1.item == itemType.stonePillar2)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = true;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }

            if (inventory1.item == itemType.stonePillar3)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = true;
                stonePillar4.enabled = false;
            }

            if (inventory1.item == itemType.stonePillar4)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = true;
            }

            if (inventory1.item == itemType.empty)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }
        }

        if (gameplayer == false)
        {
            if (inventory2.item == itemType.woodPillar)
            {
                woodPillar.enabled = true;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }

            if (inventory2.item == itemType.stonePillar)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = true;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }

            if (inventory2.item == itemType.longwoodPillar)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = true;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }

            if (inventory2.item == itemType.staticMoon)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }

            if (inventory2.item == itemType.treebox)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = true;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }

            if (inventory2.item == itemType.longStonePillar)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = true;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }

            if (inventory2.item == itemType.stonePillar2)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = true;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }

            if (inventory2.item == itemType.stonePillar3)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = true;
                stonePillar4.enabled = false;
            }

            if (inventory2.item == itemType.stonePillar4)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = true;
            }

            if (inventory2.item == itemType.empty)
            {
                woodPillar.enabled = false;
                stonePillar.enabled = false;
                longStonePillar.enabled = false;
                longwoodPillar.enabled = false;
                treeBox.enabled = false;
                stonePillar2.enabled = false;
                stonePillar3.enabled = false;
                stonePillar4.enabled = false;
            }
        }
    }

    #region SetEnun

    public void SetEnumValueToWoodPillar()
    {
        if (gameplayer == true)
        {
            inventory1.item = itemType.woodPillar;
        }

        if (gameplayer == false)
        {
            inventory2.item = itemType.woodPillar; ;
        }

    }

    public void SetEnumValueToStonePillar()
    {
        if (gameplayer == true)
        {
            inventory1.item = itemType.stonePillar;
        }

        if (gameplayer == false)
        {
            inventory2.item = itemType.stonePillar;
        }
    }

    public void SetEnumValueToLongStonePillar()
    {
        if (gameplayer == true)
        {
            inventory1.item = itemType.longStonePillar;
        }

        if (gameplayer == false)
        {
            inventory2.item = itemType.longStonePillar;
        }
    }

    public void SetEnumValueToLongWoodPillar()
    {
        if (gameplayer == true)
        {
            inventory1.item = itemType.longwoodPillar;
        }

        if (gameplayer == false)
        {
            inventory2.item = itemType.longwoodPillar;
        }
    }

    public void SetEnumValueToStonePillar2()
    {
        if (gameplayer == true)
        {
            inventory1.item = itemType.stonePillar2;
        }

        if (gameplayer == false)
        {
            inventory2.item = itemType.stonePillar2;
        }
    }

    public void SetEnumValueToStonePillar3()
    {
        if (gameplayer == true)
        {
            inventory1.item = itemType.stonePillar3;
        }

        if (gameplayer == false)
        {
            inventory2.item = itemType.stonePillar3;
        }
    }

    public void SetEnumValueToStonePillar4()
    {
        if (gameplayer == true)
        {
            inventory1.item = itemType.stonePillar4;
        }

        if (gameplayer == false)
        {
            inventory2.item = itemType.stonePillar4;
        }
    }

    public void SetEnumValueTotreebox()
    {
        if (gameplayer == true)
        {
            inventory1.item = itemType.treebox;
        }

        if (gameplayer == false)
        {
            inventory2.item = itemType.treebox;
        }
    }

    public void SetEnumValueToEmpty()
    {
        if (gameplayer == true)
        {
            inventory1.item = itemType.empty;
        }

        if (gameplayer == false)
        {
            inventory2.item = itemType.empty;
        }
    }

    #endregion
}
