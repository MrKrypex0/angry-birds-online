using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Networking;
using DapperDino.Mirror.Tutorials.Lobby;

public class Enemy : NetworkBehaviour
{
	[Header("Values")]
	public float health = 4f;
	public String whatObject;
	public string multiplayer;

	[Header("Components")]
	[SerializeField] AudioSource sound;
	public ParticleSystem deathEffect;


	void Start()
	{
		sound = GameObject.FindGameObjectWithTag("destroysound").GetComponent<AudioSource>();
	}

	void Die()
	{
		if (whatObject != "enemy")
		{
			sound.Play();
		}
	    Destroy(gameObject);
	}

	void DieOnServer()
    {
		NetworkServer.Destroy(gameObject);
	}

	[Command(ignoreAuthority = true)]
	void CmdDieOnClient()
    {
		NetworkServer.Destroy(gameObject);
	}
	
	void OnCollisionEnter2D(Collision2D colInfo)
	{
		if(multiplayer != "yes")
        {
			if (colInfo.relativeVelocity.magnitude > health)
			{
				//Instantiate(deathEffect, transform.position, Quaternion.identity);
				Die();
			}

		}

		if (multiplayer == "yes")
        {
			if (colInfo.gameObject.tag == "Character1")
			{
				if (isClientOnly == true)
				{
					print("client");
					CmdDieOnClient();
				}
                
				if(isServerOnly == true)
                {
					print("server");
					DieOnServer();
				}
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "explosion")
		{
		    Instantiate(deathEffect, transform.position, Quaternion.identity);
			Die();
		}
	}

}
