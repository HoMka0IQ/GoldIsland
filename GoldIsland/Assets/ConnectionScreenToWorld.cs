using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionScreenToWorld : MonoBehaviour
{
    public Vector3 targetPos = new Vector3();
    public Vector3 moveFromCenter;
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        transform.position = cam.WorldToScreenPoint(targetPos + moveFromCenter);
    }
}
