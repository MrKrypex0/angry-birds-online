using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [Header("Values")]
    public float speed;

    [Header("Components")]
    public Rigidbody2D rb;

    void Update()
    {
        MoveFunction();
    }

    private void MoveFunction()
    {
        rb.velocity = transform.right * speed;
    }
}
