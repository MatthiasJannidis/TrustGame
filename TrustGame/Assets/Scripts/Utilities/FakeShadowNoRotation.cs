using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FakeShadowNoRotation : MonoBehaviour
{
    [SerializeField] Transform tooCloseFallbackTransform;
    [SerializeField] Transform tooCloseFallbackPivot;

    private Transform lightEmitter;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Player p = FindObjectOfType<Player>();
        lightEmitter = p.transform;
    }

    private void Update()
    {
        Vector3 lookAt = lightEmitter.position - tooCloseFallbackPivot.position;
        float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg + 90.0f;

        Vector3 fallbackDelta = tooCloseFallbackTransform.position - tooCloseFallbackPivot.position;
        float fallbackAngle = Mathf.Atan2(fallbackDelta.y, fallbackDelta.x) * Mathf.Rad2Deg + 90.0f;
        
        tooCloseFallbackPivot.rotation = Quaternion.Euler(.0f, .0f, angle);


        if (lookAt.magnitude < fallbackDelta.magnitude) 
        {
            transform.rotation = Quaternion.Euler(.0f, .0f, fallbackAngle);
        } else 
        {
            transform.rotation = Quaternion.Euler(.0f, .0f, angle);
        }

       
        lineRenderer.endWidth = 10.0f + 5.0f / lookAt.sqrMagnitude;
    }
}
