using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = -10f;
    public float moveDuration = 0.2f;
    public Vector3 velocity = Vector3.zero;

    void Start()
    {

    }

    void FixedUpdate()
    {
        Vector3 destination = target.TransformPoint(new Vector3(1, 1, distance));
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, moveDuration);
    }
}
