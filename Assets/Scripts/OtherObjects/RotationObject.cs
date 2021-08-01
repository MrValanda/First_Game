using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform _transform;
    // Update is called once per frame
    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
     _transform.Rotate(Vector3.up * speed);   
    }
}
