using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    private float backgroundSize;

    private Transform cameraTransform;
    private Transform[] layers;

    private float viewZone = 5;

    private int leftIndex;
    private int rightIndex;

    // Use this for initialization
    void Start() {
        cameraTransform = Camera.main.transform;
        layers = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;

        backgroundSize = layers[0].transform.GetComponent<Renderer>().bounds.size.x;

        Debug.Log("computed backgroundSize: " + backgroundSize);
        Debug.Log("computed xpositions: " + layers[0].transform.position.x + " -- " + layers[1].transform.position.x + " -- " + layers[2].transform.position.x);
	}
	
	// Update is called once per frame
	void Update () {
        float cameraPosX = cameraTransform.position.x;
        float leftThreshold = layers[leftIndex].transform.position.x - viewZone;
        float rightThreshold = layers[rightIndex].transform.position.x + viewZone;

        if (cameraPosX < leftThreshold)
        {
            ScrollLeft();
        } else if (cameraPosX > rightThreshold)
        {
            ScrollRight();
        } else
        {
            //Debug.Log("didn't scroll. cameraPosX: " + cameraPosX + " -- thresholds: " + leftThreshold + ", " + rightThreshold);
        }
    }

    private void ScrollLeft()
    {
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;

        if (rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }

    private void ScrollRight()
    {
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        Debug.Log("moved to position: " + layers[leftIndex].position.x);
        rightIndex = leftIndex;
        leftIndex++;

        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }
}
