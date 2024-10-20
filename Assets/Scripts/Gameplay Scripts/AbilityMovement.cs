using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMovement : MonoBehaviour
{
    public int speed;
    public Rigidbody2D rb;
    public string whaObject;
    public int time;
    [SerializeField] Collider2D col;
    [SerializeField] CameraMovement camMovement;
    [SerializeField] AudioSource sound;
    [SerializeField] Animator anim;

    private void Start()
    {
        camMovement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        sound = GameObject.FindGameObjectWithTag("HitAudio").GetComponent<AudioSource>();

        if(whaObject == "magicwall")
        {
            camMovement.shootShake = true;
        }

        if(whaObject == "spike")
        {
            camMovement.shootShake = true;
            sound.Play();
        }

        if(whaObject == "bomb")
        {
            anim = GetComponent<Animator>();
            col = GetComponent<CircleCollider2D>();
            BombAbility();
        }
        else
        {
            col = null;
        }
    }

    private void BombAbility()
    {
        anim.SetTrigger("expand");
        Destroy(gameObject, 1);
    }
    private void Update()
    {
        if(whaObject == "comet")
        {
            rb.velocity = transform.right * speed;
        }

        if(whaObject == "magicwall")
        {
            rb.velocity = transform.right * speed;
            Destroy(gameObject, 0.1f);
        }

        if(whaObject == "spike")
        {
            rb.velocity = transform.up * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Envoirment")
        {
            if (whaObject == "comet") 
            {
                speed = 0;
                camMovement.shootShake = true;
                sound.Play();
                Destroy(gameObject, 1);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Envoirment")
        {
            if (whaObject == "spike")
            {
                Destroy(gameObject);
            }
        }
    }
}
