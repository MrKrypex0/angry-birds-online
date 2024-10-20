using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLinesBehavior : MonoBehaviour
{
    [SerializeField] ParticleSystem particels;

    void Start()
    {
        particels = GetComponent<ParticleSystem>();
        particels.Stop();
    }
}
