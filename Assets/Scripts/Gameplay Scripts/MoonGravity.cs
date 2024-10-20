using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonGravity : MonoBehaviour
{
    
    Rigidbody2D rb;

    [SerializeField] bool trappedInFeild = false;

    Vector2 lookDirection;

    float lookAngle;

    [Header ("Gravity")]
    
    // Distance where gravity works
    [Range(0.0f, 1000.0f)]
    public float maxGravDist = 150.0f;
    
    // Gravity force
    [Range(0.0f, 1000.0f)]
    public float maxGravity = 50.0f;
    
    // Your planet
    [SerializeField] GameObject planet;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        planet = GameObject.FindGameObjectWithTag("planet");
    }

    private void Update()
    {
        if(trappedInFeild == true)
        {
            // Distance to the planet
            float dist = Vector3.Distance(planet.transform.position, transform.position);

            // Gravity
            Vector3 v = planet.transform.position - transform.position;
            rb.AddForce(v.normalized * (1.0f - dist / maxGravDist) * maxGravity);

            // Rotating to the planet
            lookDirection = planet.transform.position - transform.position;
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "planet")
        {
            trappedInFeild = true;
        }
    }
}
