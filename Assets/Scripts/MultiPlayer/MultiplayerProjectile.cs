using DapperDino.Mirror.Tutorials.Lobby;
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerProjectile : NetworkBehaviour
{
	[Header("Values")]
	public float releaseTime = .15f;
	public float maxDragDistance = 2f;
	[SyncVar]
	public bool isPressed = false;
	[SyncVar]
	public bool hasHitGround = false;
	public bool notGrounded;
	public string whatCharacter;
	[SyncVar]
	public bool oneTime = false;

	[Header("Components")]
	public Rigidbody2D rb;
	public Rigidbody2D hook;
	public SpringJoint2D joint;
	public LineRenderer catapultLineFront;
	public LineRenderer catapultLineBack;
	public Transform catapult;
	[SerializeField] AudioSource sound;
    [SyncVar]
	public bool released = false;
	public Ray leftCatapultToProjectile;
	//public MultiplayerShootProjectory project;
	public MultiplayerShootProjectory shoot;
	[SerializeField] SpecialPowerScript sPower;
	[SerializeField] ParticleSystem particels;
	[SerializeField] RoundScript roundScript;
	[SerializeField] private NetworkGamePlayerLobby gamePlayer;
	[SerializeField] private NetworkTransform networkTransform;

	private void Start()
	{
		StartCode();
	}

	private void StartCode()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.isKinematic = true;
		roundScript = GameObject.FindGameObjectWithTag("roundmanager").GetComponent<RoundScript>();
		particels = GameObject.FindGameObjectWithTag("chargeeffect").GetComponent<ParticleSystem>();
		sPower = GetComponent<SpecialPowerScript>();
		joint = GetComponent<SpringJoint2D>();
		sound = GameObject.FindGameObjectWithTag("HitAudio").GetComponent<AudioSource>();
		leftCatapultToProjectile = new Ray(catapultLineFront.transform.position, Vector3.zero);
	}

    void Update()
	{
		if (isServer)
        {
			if(roundScript.player1 == true)
            {
				if (gamePlayer == null)
				{
					gamePlayer = GameObject.FindGameObjectWithTag("Player1").GetComponent<NetworkGamePlayerLobby>();
				}

				if(gamePlayer.isLeader == true)
                {
					if (isPressed == true)
					{
						//project.RpcSpawnPoints();
						RpcDragging();
					}
				}
			}
        }

        if (isClient)
        {
			if (roundScript.player2 == true)
			{
				if (gamePlayer == null)
				{
					gamePlayer = GameObject.FindGameObjectWithTag("Player2").GetComponent<NetworkGamePlayerLobby>();
				}

				if (gamePlayer.isLeader == false)
				{
					if (isPressed == true)
					{
						//project.RpcSpawnPoints();
						RpcDragging();
					}
				}
			}
		}

		RpcUpdateGameFunction();
		RpcLineRendererUpdate();

		if (isPressed == false)
        {
			LineRendererSetup();
		}
	}

	private void RpcUpdateGameFunction()
    {
		if (roundScript.player1 == true)
        {
			shoot = GameObject.FindGameObjectWithTag("Catapult1").GetComponent<MultiplayerShootProjectory>();
            catapultLineFront = GameObject.FindGameObjectWithTag("CatapultFront1").GetComponent<LineRenderer>();
            catapultLineBack = GameObject.FindGameObjectWithTag("CatapultBack1").GetComponent<LineRenderer>();
            //project = GameObject.FindGameObjectWithTag("Catapult1").GetComponent<MultiplayerShootProjectory>();
            catapult = GameObject.FindGameObjectWithTag("Catapult1").GetComponent<Transform>();
            hook = GameObject.FindGameObjectWithTag("Catapult1").GetComponent<Rigidbody2D>();
			gamePlayer = GameObject.FindGameObjectWithTag("Player1").GetComponent<NetworkGamePlayerLobby>();
		}

        if (roundScript.player2 == true)
        {
			shoot = GameObject.FindGameObjectWithTag("Catapult2").GetComponent<MultiplayerShootProjectory>();
            catapultLineFront = GameObject.FindGameObjectWithTag("CatapultFront2").GetComponent<LineRenderer>();
            catapultLineBack = GameObject.FindGameObjectWithTag("CatapultBack2").GetComponent<LineRenderer>();
            //project = GameObject.FindGameObjectWithTag("Catapult2").GetComponent<MultiplayerShootProjectory>();
            catapult = GameObject.FindGameObjectWithTag("Catapult2").GetComponent<Transform>();
            hook = GameObject.FindGameObjectWithTag("Catapult2").GetComponent<Rigidbody2D>();
			gamePlayer = GameObject.FindGameObjectWithTag("Player2").GetComponent<NetworkGamePlayerLobby>();
		}

        if (roundScript.player1 == true)
        {
            if (gamePlayer.isLeader == true)
            {
                if (isServer == true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        isPressed = true;
                        rb.isKinematic = true;
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        released = true;
                        StartCoroutine(Release());
                    }
                }
            }

        }

        if (roundScript.player2 == true)
        {
            if (gamePlayer.isLeader == false)
            {
                if (isServer == false)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        CmdPressed();
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        CmdRealesed();
                    }
                }
            }

        }

        if (isPressed == true)
        {
            if (released == true)
            {
                if (oneTime == false)
                {
                    DeletePoints();
                    rb.isKinematic = false;
                }
            }
        }

        if (hasHitGround == true)
        {
            if (whatCharacter == "astroplayer")
            {
                particels.Stop();
                Destroy(gameObject, 5);
            }

            if (whatCharacter != "astroplayer")
            {
                if (sPower.abilityActivated == false)
                {
                    particels.Stop();
                    Destroy(gameObject, 5);
                }
            }
        }

        if (released == false)
        {
            if (joint == null)
            {
                joint = GetComponent<SpringJoint2D>();
            }
        }

        if (isPressed == true && released == true)
        {
            catapultLineFront.enabled = false;
            catapultLineBack.enabled = false;
        }

        if (isPressed == true && released == false)
        {
            catapultLineFront.enabled = true;
            catapultLineBack.enabled = true;
        }
    }

    [Command(ignoreAuthority = true)]
	private void CmdRealesed()
    {
		released = true;
		StartCoroutine(Release());
    }

    [Command(ignoreAuthority = true)]
    private void CmdPressed()
    {
        isPressed = true;
        rb.isKinematic = true;
    }

    private void RpcDragging()
	{
		if (isPressed)
		{
			if (released == false)
			{
				rb.gravityScale = 1f;

				Vector2 mousePos = GetWorldPositionOnPlane(0);
				if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
				{
					rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
				}
				else
				{
					rb.position = mousePos;
				}

                if (roundScript.player2 == true)
                {
                    Vector2 ServermousePos = GetWorldPositionOnPlane(0);

                    CmdDragging(ServermousePos);
                }
            }
		}
	}

    [Command(ignoreAuthority = true)]
    private void CmdDragging(Vector2 mousePos)
    {

        if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
        {
            rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
        }
        else
        {
            rb.position = mousePos;
        }
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

		RpcShoot();
		yield return new WaitForSeconds(1f);
    }

	private void RpcShoot()
    {
		if(roundScript.player1 == true)
        {
			Vector2 forceUp = new Vector2(rb.velocity.x * -1f, 4f);
			GetComponent<SpringJoint2D>().enabled = false;
			notGrounded = true;
			rb.AddForce(rb.velocity * forceUp, ForceMode2D.Impulse);
			oneTime = true;
		}

		if (roundScript.player2 == true)
		{
			Vector2 forceUp = new Vector2(rb.velocity.x, 4f);
			GetComponent<SpringJoint2D>().enabled = false;
			notGrounded = true;
			rb.AddForce(rb.velocity * forceUp, ForceMode2D.Impulse);
			oneTime = true;
		}
	}

	public void DeletePoints()
	{
		GameObject[] points = GameObject.FindGameObjectsWithTag("Point");

		foreach (GameObject target in points)
		{
			Destroy(target);
		}
	}

    void RpcLineRendererUpdate()
    {
		Vector2 catapultToProjectile = transform.position; //+ catapultLineFront.transform.position;
		leftCatapultToProjectile.direction = catapultToProjectile;
		Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + 0.25f);
		catapultLineFront.SetPosition(1, holdPoint);
		catapultLineBack.SetPosition(1, holdPoint);
	}

    public void LineRendererSetup()
	{
		joint.connectedBody = hook;
		catapultLineFront.SetPosition(0, catapultLineFront.transform.position);
		catapultLineBack.SetPosition(0, catapultLineBack.transform.position);

		catapultLineFront.sortingLayerName = "Deafult";
		catapultLineBack.sortingLayerName = "Deafult";

		catapultLineFront.sortingOrder = 2;
		catapultLineBack.sortingOrder = 1;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Envoirment")
		{
			if (hasHitGround != true)
			{
				notGrounded = false;
				//camMovement.shootShake = true;
				sound.Play();
				hasHitGround = true;
				Destroy(gameObject);
			}
		}

		if (collision.gameObject.tag == "Pillar")
		{
			if (hasHitGround != true)
			{
				//camMovement.shootShake = true;
				hasHitGround = true;
			}
			sound.Play();
			Destroy(gameObject);
		}

		if(collision.gameObject.tag == "barrier")
        {
			if (hasHitGround != true)
			{
				notGrounded = false;
				//camMovement.shootShake = true;
				sound.Play();
				hasHitGround = true;
				Destroy(gameObject);
			}
		}
	}

	public Vector3 GetWorldPositionOnPlane(float z)
	{
		Vector2 touch = Input.mousePosition;
		Ray ray = Camera.main.ScreenPointToRay(touch);
		Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
		float distance;
		xy.Raycast(ray, out distance);
		return ray.GetPoint(distance);
	}
}
