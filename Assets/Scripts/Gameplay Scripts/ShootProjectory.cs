using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ShootProjectory : MonoBehaviour
{
    [Header("Ints")]
    public int numberOfPoints;

    [Header("Floats")]
    public float force;

    [Header("Components")]
    public GameObject pointPrefab;
    public GameObject[] points;
    public GameObject canvas;
    [SerializeField] Projectile project;

    [Header("Vectors")]
    Vector2 direction;

    private void Update()
    {
        if(project != null)
        {
            UpDateProjectoryFunction();
        }
    }

    public void FindPlayer()
    {
        project = GameObject.FindGameObjectWithTag("Character1").GetComponent<Projectile>();
    }

    public void SpawnPoints()
    {
        points = new GameObject[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
        }
    }
    private void UpDateProjectoryFunction()
    {
        if (project.isPressed == true)
        {

            Vector2 mousePos = GetWorldPositionOnPlane(0);

            Vector2 bowPos = transform.position;

            direction = mousePos - bowPos;

            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].transform.position = PointPosition(i * 0.095f);
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

    Vector2 PointPosition(float t)
    {
        Vector2 currentPointPos = (Vector2)transform.position + (-direction.normalized * force * t) + 0.5f * Physics2D.gravity * (t*t);

        return currentPointPos;
    }
}
