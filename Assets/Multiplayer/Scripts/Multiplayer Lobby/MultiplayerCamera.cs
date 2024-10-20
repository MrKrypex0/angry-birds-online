using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerCamera : NetworkBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform targetTransfrom;
    [SerializeField] private CinemachineVirtualCamera virtualcamera = null;

    private CinemachineTransposer transposer;

    [Header("Bools")]
    [SerializeField] public bool bulletShake = false;
    [SerializeField] public bool abilityShake = false;
    [SerializeField] public bool shootShake = false;
    [SerializeField] public bool menyShake = false;
    [SerializeField] public bool zoomshake = false;

    [Header("Floats")]
    public float minZoom = 40f;
    public float maxZoom = 60f;
    public float zoomLimiter = 50f;
    float duration = 0.1f;
    float power;
    float slowDownamount;
    float initailDuration;
    float smoothTime = 0.2f;

    [Header("Vectors")]
    public Vector3 offset = new Vector3(9, 1.5f, 2);
    public Vector3 startOffset;
    Vector3 velocity;

    [Header("Components")]
    public List<GameObject> targets;

    public override void OnStartAuthority()
    {
        targetTransfrom = targets[0].transform;
        transposer = virtualcamera.GetCinemachineComponent<CinemachineTransposer>();

        virtualcamera.gameObject.SetActive(true);

        enabled = true;
    }


    private void Update()
    {
        if(targets.Count == 0)
        {
            targets.Add(GameObject.FindGameObjectWithTag("focus"));
        }
    }

    //void FixedUpdate()
    //{
    //    if (targets.Count == 0)
    //        return;

    //    MoveFunction();
    //}
    public void CamerShake()
    {
        if (duration > 0)
        {
            virtualcamera.gameObject.transform.localPosition = transform.position + UnityEngine.Random.insideUnitSphere * power;
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
            virtualcamera.gameObject.transform.localPosition = transform.position;
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
            return targets[0].transform.position;
        }

        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }

        return bounds.center;
    }

    //private void Zoom()
    //{
    //    float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);

    //    virtualcamera.fieldOfView = Mathf.Lerp(cams.fieldOfView, newZoom, Time.fixedDeltaTime);
    //}

    public float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }

        return bounds.size.x;
    }

    void MoveFunction()
    {
        Vector3 centerpoint = targetTransfrom.position;

        Vector3 newPos = centerpoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
    }

    public void MenyCamerShake()
    {
        menyShake = true;
    }
}
