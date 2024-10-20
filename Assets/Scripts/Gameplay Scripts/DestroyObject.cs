using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [Header("Values")]
    public int time;

    void Update()
    {
        DestroyGameObject();
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject, time);
    }
}
