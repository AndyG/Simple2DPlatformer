using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSlow : MonoBehaviour {

	public float degreesPerSec = 0f; 

	void Start() { 
	} 

	void Update() { 
		float rotAmount = degreesPerSec * Time.deltaTime; 
		float curRot = transform.localRotation.eulerAngles.z; 
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, curRot + rotAmount));
	} 
}
