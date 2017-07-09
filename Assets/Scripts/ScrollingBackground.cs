using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    public float parallaxCoefficient;

    private float backgroundSize;

    private Transform cameraTransform;
    private Transform[] layers;

    private float viewZone = 4;

    private int leftIndex;
    private int rightIndex;

    private float lastCameraX;
    private float lastCameraY;

    // Use this for initialization
    void Start() {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        layers = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;

        backgroundSize = layers[0].transform.GetComponent<Renderer>().bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
        float deltaX = cameraTransform.position.x - lastCameraX;
        float deltaY = cameraTransform.position.y - lastCameraY;

        transform.position += Vector3.right * (deltaX * parallaxCoefficient);
        transform.position += Vector3.down * (deltaY * parallaxCoefficient);

        float leftThreshold = layers[leftIndex].transform.position.x - viewZone;
        float rightThreshold = layers[rightIndex].transform.position.x + viewZone;

        if (lastCameraX < leftThreshold)
        {
            ScrollLeft();
        } else if (lastCameraX > rightThreshold)
        {
            ScrollRight();
        }

        lastCameraX = cameraTransform.position.x;
        lastCameraY = cameraTransform.position.y;
    }

    private void ScrollLeft()
    {
        Vector3 newPosition = new Vector3(
            layers[leftIndex].position.x - backgroundSize,
            layers[rightIndex].position.y,
            layers[rightIndex].position.z);

        layers[rightIndex].position = newPosition;
        leftIndex = rightIndex;
        rightIndex--;

        if (rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }

    private void ScrollRight()
    {
        Vector3 newPosition = new Vector3(
            layers[rightIndex].position.x + backgroundSize,
            layers[leftIndex].position.y,
            layers[leftIndex].position.z);

        layers[leftIndex].position = newPosition;

        rightIndex = leftIndex;
        leftIndex++;

        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }
}
