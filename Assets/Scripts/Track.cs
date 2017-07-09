using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{

    public GameObject target;
    public float distance = 15;
    public float lerpFactor = 0.1f;

    public bool isSmooth = true;

    // Use this for initialization
    void Start() { }


    // Update is called once per frame
    void Update()
    {
        if (!isSmooth)
        {
            transform.position = computeTargetPosition();
        }
    }

    void FixedUpdate()
    {
        if (isSmooth)
        {
            transform.position = Vector3.Lerp(transform.position, computeTargetPosition(), lerpFactor);
        }
    }

    private Vector3 computeTargetPosition()
    {
        return target.transform.position + (Vector3.back * distance);
    }
}
