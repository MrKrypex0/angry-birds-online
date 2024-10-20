using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Transform cam;
    [SerializeField] Camera cams;
    public List<Transform> targets;

    [Header("Floats")]
    public float minZoom = 40f;
    public float maxZoom = 60f;
    public float zoomLimiter = 50f;
    float duration = 0.1f;
    float power;
    float slowDownamount;
    float initailDuration;
    float smoothTime = 0.2f;

    [Header("Bools")]
    [SerializeField] public bool bulletShake = false;
    [SerializeField] public bool abilityShake = false;
    [SerializeField] public bool shootShake = false;
    [SerializeField] public bool menyShake = false;
    [SerializeField] public bool zoomshake = false;

    [Header("Vectors")]
    public Vector3 offset = new Vector3(9, 1.5f, 2);
    public Vector3 startOffset;
    Vector3 velocity;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Application.targetFrameRate = 60;
        }
    }

    void Start()
    {
        StartCode();
    }
    public void Update()
    {
        BoolVal();
        UpdateListFunction();
        if(SceneManager.GetActiveScene().name != "Scene_Map_01")
        {
            if (targets.Count == 0)
            {
                targets.Add(GameObject.FindGameObjectWithTag("Character").GetComponent<Transform>());
            }
        }
    }
    void FixedUpdate()
    {
        if (targets.Count == 0)
            return;

        MoveFunction();
        Zoom();
    }

    private void StartCode()
    {
        cam = GetComponent<Transform>();
        cams = GetComponent<Camera>();
        initailDuration = duration;
        startOffset = offset;
    }

    private void BoolVal()
    {
        if(bulletShake == true)
        {
            slowDownamount = 1f;
            power = 0.1f;
            CamerShake();
        }

        if(abilityShake == true)
        {
            power = 0.35f;
            slowDownamount = 2;
            CamerShake();
        }

        if(shootShake == true)
        {
            power = 0.2f;
            slowDownamount = 1.5f;
            CamerShake();
        }

        if(menyShake == true)
        {
            power = 7.5f;
            slowDownamount = 1;
            CamerShake();
        }

        if (zoomshake == true)
        {
            transform.position = new Vector3(transform.position.x, targets[1].transform.position.y, transform.position.z);
            offset = new Vector3(offset.x, targets[1].transform.position.y, offset.z); 
            power = 0.05f;
            slowDownamount = 9f;
            CamerShake();
        }

    }

    private void UpdateListFunction()
    {
        if(targets.Count >= 1)
        {
            targets.RemoveAll(item => item == null);
        }
    }

    public void CamerShake()
    {
        if (duration > 0)
        {
            cam.localPosition = transform.position + UnityEngine.Random.insideUnitSphere * power;
            duration -= Time.deltaTime / slowDownamount;
        }
        else
        {
            bulletShake = false;
            abilityShake = false;
            shootShake = false;
            menyShake = false;
            zoomshake = false;
            duration = initailDuration;
            cam.localPosition = transform.position;
            StartCoroutine(OffsetAfterAbility());
        }
    }

    IEnumerator OffsetAfterAbility()
    {
        yield return new WaitForSeconds(2);
        offset = startOffset;
    }

    public Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
           return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);

        cams.fieldOfView = Mathf.Lerp(cams.fieldOfView, newZoom, Time.fixedDeltaTime);
    }

    public float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    void MoveFunction()
    {
        Vector3 centerpoint = GetCenterPoint();

        Vector3 newPos = centerpoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
    }

    public void MenyCamerShake()
    {
        menyShake = true;
    }
}

    

