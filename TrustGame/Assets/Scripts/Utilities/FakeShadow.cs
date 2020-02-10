using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FakeShadow : MonoBehaviour
{

    private Transform lightEmitter;
    private LineRenderer lineRenderer;
    private int myValue = 0;


    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Player p = FindObjectOfType<Player>();
        lightEmitter = p.GetFlashlightTransform();
    }

    private void Update()
    {
        Vector3 lookAt = lightEmitter.position - transform.position;
        float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(.0f, .0f, angle + 90.0f);
        lineRenderer.endWidth = 10.0f+ 5.0f / lookAt.sqrMagnitude;
    }

}
