using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHorizMove : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float horizForce = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizForce * speed * Time.deltaTime, 0, 0);
    }
}
