using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Vector3 offset;
    [SerializeField] private GameObject target;
    
    
    void Start()
    {
        offset = transform.position - target.transform.position;
    }


    private void LateUpdate()
    {
        var newPosition = target.transform.position + offset;
        transform.position = newPosition;
    }
}
