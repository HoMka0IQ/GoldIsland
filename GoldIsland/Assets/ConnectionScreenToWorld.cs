using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionScreenToWorld : MonoBehaviour
{
    public GameObject target;
    public Vector3 moveFromCenter;
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = cam.WorldToScreenPoint(target.transform.position + moveFromCenter);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
