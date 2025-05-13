using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float minCuttingVelocity = 0.001f;
    public TrailRenderer trail;
    private Camera mainCamera;
    private Vector3 previousPosition;
    private Rigidbody bladeRb;
    private Collider bladeCollider;
    private bool isCutting = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        bladeRb = GetComponent<Rigidbody>();
        bladeCollider = GetComponent<Collider>();
        bladeCollider.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isCutting)
        {
            UpdateCut();
        }

    }

    void StartCutting()
    {
        isCutting = true;
        trail.enabled = true;
        previousPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        transform.position = previousPosition;
    }

     void StopCutting()
    {
        isCutting = false;
        trail.enabled = false;
        bladeCollider.enabled = false;
    }

     void UpdateCut()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        bladeRb.position = newPosition;

        float velocity = (newPosition - previousPosition).magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minCuttingVelocity;

        previousPosition = newPosition;
    }


}
