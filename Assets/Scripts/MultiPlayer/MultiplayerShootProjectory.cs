using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerShootProjectory : NetworkBehaviour
{
    [Header("Ints")]
    public int numberOfPoints;
    public int wichCatapult;

    [Header("Floats")]
    public float force;

    [Header("Bools")]

    private bool oneTime = false;

    [Header("Components")]
    public GameObject pointPrefab;
 
    public GameObject[] points;
    public GameObject canvas;

    [SerializeField] MultiplayerProjectile project = null;
    [SerializeField] private RoundScript roundScript;

    [Header("Vectors")]
    Vector2 direction;

    [SyncVar]
    [SerializeField] Vector2 currentPointPos;


    private void Update()
    {
        if (roundScript == null)
        {
            roundScript = GameObject.FindGameObjectWithTag("roundmanager").GetComponent<RoundScript>();
        }

        if (wichCatapult == 2)
        {
            if (roundScript.player1 == true)
            {
                project = null;
            }
        }

        if (wichCatapult == 1)
        {
            if (roundScript.player2 == true)
            {
                project = null;
            }
        }

        if (project == null)
        {
            if (wichCatapult == 1)
            {
                if (roundScript.player1 == true)
                {
                    project = GameObject.FindGameObjectWithTag("Character1").GetComponent<MultiplayerProjectile>();
                }
            }

            if (wichCatapult == 2)
            {
                if (roundScript.player2 == true)
                {
                    project = GameObject.FindGameObjectWithTag("Character1").GetComponent<MultiplayerProjectile>();
                }
            }
        }

        if (project != null)
        {
            if (isServer)
            {
                if (roundScript.player1 == true)
                {
                    RpcUpDateProjectoryFunction();
                }
            }

            if (isClientOnly)
            {
                if (roundScript.player2 == true)
                {
                    CmdUpDateProjectoryFunction();
                }
            }
        }

    }

    [ClientRpc]
    public void RpcSpawnPoints()
    {
        if(oneTime == false)
        {
            if (project.isPressed == true)
            {
                points = new GameObject[numberOfPoints];

                for (int i = 0; i < numberOfPoints; i++)
                {
                    points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
                }
                oneTime = true;
            }
        }
    }

    [Command (ignoreAuthority = true)]
    public void CmdSpawnPoints()
    {
        if(oneTime == false)
        {
            if (project.isPressed == true)
            {
                points = new GameObject[numberOfPoints];

                for (int i = 0; i < numberOfPoints; i++)
                {
                    points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
                }
                oneTime = true;
            }
        }
    }

    [ClientRpc]
    private void RpcUpDateProjectoryFunction()
    {
        if (project.isPressed == true)
        {
            if(project.released == false)
            {
                Vector2 mousePos = GetWorldPositionOnPlane(0);

                Vector2 bowPos = transform.position;

                direction = mousePos - bowPos;

                for (int i = 0; i < numberOfPoints; i++)
                {
                    points[i].transform.position = RpcPointPosition(i * 0.095f);
                }
            }
        }
    }

    [Command (ignoreAuthority = true)]
    private void CmdUpDateProjectoryFunction()
    {
        if (project.isPressed == true)
        {
            if (project.released == false)
            {
                Vector2 mousePos = GetWorldPositionOnPlane(0);

                Vector2 bowPos = transform.position;

                direction = mousePos - bowPos;

                for (int i = 0; i < numberOfPoints; i++)
                {
                    points[i].transform.position = RpcPointPosition(i * 0.095f);
                }
            }
        }
    }

    public Vector3 GetWorldPositionOnPlane(float z)
    {
        //Touch touch = Input.GetTouch(0);
        Vector2 touch = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(touch);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    public Vector2 RpcPointPosition(float t)
    {
        currentPointPos = (Vector2)transform.position + (-direction.normalized * force * t) + 0.5f * Physics2D.gravity * (t * t);

        return currentPointPos;
    }
}
