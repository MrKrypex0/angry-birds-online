using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
	[Header("Values")]
	public float releaseTime = .15f;
	public float maxDragDistance = 2f;
	public bool isPressed = false;
	public bool hasHitGround = false;
	public bool notGrounded;
	public string whatCharacter;

	[Header("Components")]
	public Rigidbody2D rb;
	public Rigidbody2D hook;
	public SpringJoint2D joint;
	public LineRenderer catapultLineFront;
	public LineRenderer catapultLineBack;
	public Transform catapult;
	[SerializeField] AudioSource sound;
	public bool released = false;
	public ParticleSystem hitParticels;
	public Ray leftCatapultToProjectile;
	public ShootProjectory project;
	public CameraMovement camMovement;
	public GameObject nextBall;
	public ShootProjectory shoot;
	public AddAmmo amo;
	[SerializeField] SpecialPowerScript sPower;
	[SerializeField] ParticleSystem particels;



	private void Start()
	{
		StartCode();
	}

	private void StartCode()
	{
		particels = GameObject.FindGameObjectWithTag("chargeeffect").GetComponent<ParticleSystem>();
		sPower = GetComponent<SpecialPowerScript>();
		rb = GetComponent<Rigidbody2D>();
		shoot = GameObject.FindGameObjectWithTag("Catapult").GetComponent<ShootProjectory>();
		catapultLineFront = GameObject.FindGameObjectWithTag("CatapultFront").GetComponent<LineRenderer>();
		catapultLineBack = GameObject.FindGameObjectWithTag("CatapultBack").GetComponent<LineRenderer>();
		project = GameObject.FindGameObjectWithTag("Catapult").GetComponent<ShootProjectory>();
		joint = GetComponent<SpringJoint2D>();
		camMovement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
		sound = GameObject.FindGameObjectWithTag("HitAudio").GetComponent<AudioSource>();
		leftCatapultToProjectile = new Ray(catapultLineFront.transform.position, Vector3.zero);
		amo = GameObject.FindGameObjectWithTag("AMO").GetComponent<AddAmmo>();
		catapult = GameObject.FindGameObjectWithTag("Catapult").GetComponent<Transform>();
		hook = GameObject.FindGameObjectWithTag("Catapult").GetComponent<Rigidbody2D>();
		shoot.FindPlayer();
		LineRendererSetup();
	}

	void Update()
	{
		Dragging();

		UpdateGameFunction();

		
		LineRendererUpdate();
	}

	private void UpdateGameFunction()
	{
		if(released == true)
        {
			Destroy(gameObject, 5f);
        }

		if (hasHitGround == true)
		{
			if(whatCharacter == "astroplayer")
			{
				particels.Stop();
				Destroy(gameObject, 5);
			}

			if(whatCharacter != "astroplayer")
			{
				if (sPower.abilityActivated == false)
				{
					particels.Stop();
					Destroy(gameObject, 5);
				}
			}
		}

		if (joint == null)
		{
			joint = GetComponent<SpringJoint2D>();
		}

		if (!isPressed)
		{
			catapultLineFront.enabled = false;
			catapultLineBack.enabled = false;
		}
		else
		{
			catapultLineFront.enabled = true;
			catapultLineBack.enabled = true;
		}
	}

	void OnMouseDown()
	{
		isPressed = true;
		rb.isKinematic = true;
		project.SpawnPoints();
	}
	void OnMouseUp()
	{
		DeletePoints();
		isPressed = false;
		rb.isKinematic = false;

		StartCoroutine(Release());
	}

	private void Dragging()
	{
		if (isPressed)
		{
			Vector2 mousePos = GetWorldPositionOnPlane(0);
			if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
			{
				rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
			}
			else
			{
				rb.position = mousePos;
			}
		}
	}

	IEnumerator Release()
	{
		yield return new WaitForSeconds(releaseTime);

		Vector2 forceUp = new Vector2(rb.velocity.x / 20f, 2f);

		GetComponent<SpringJoint2D>().enabled = false;
		notGrounded = true;
		rb.AddForce(rb.velocity * forceUp, ForceMode2D.Impulse);
		released = true;
		yield return new WaitForSeconds(1f);
	}

	public void DeletePoints()
	{
		GameObject[] points = GameObject.FindGameObjectsWithTag("Point");

		foreach (GameObject target in points)
		{
			Destroy(target);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Envoirment")
		{
			if (hasHitGround != true)
			{
				notGrounded = false;
				camMovement.shootShake = true;
				sound.Play();
				hasHitGround = true;
			}
		}

		if(collision.gameObject.tag == "Pillar")
		{
			if (hasHitGround != true)
			{
				camMovement.shootShake = true;
				hasHitGround = true;
			}
			sound.Play();
		}

		if (collision.gameObject.tag == "box")
		{
			if (hasHitGround != true)
			{
				camMovement.shootShake = true;
				hasHitGround = true;
			}
			sound.Play();
		}

		if (collision.gameObject.tag == "enemy")
		{
			if(hasHitGround != true)
			{
				hasHitGround = true;
				camMovement.shootShake = true;
				if (sPower.abilityActivated == false)
				{
					particels.Stop();
					Destroy(gameObject, 5);
				}

			}
			sound.Play();
		}
	}

	void LineRendererUpdate()
	{
		Vector2 catapultToProjectile = transform.position - catapultLineFront.transform.position;
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
