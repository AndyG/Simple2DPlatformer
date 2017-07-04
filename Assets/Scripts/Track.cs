using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {

	public GameObject target;
	public float distance = 15;

	// Use this for initialization
	void Start () {
	//	target = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerInfo = target.transform.transform.position;

		transform.position = new Vector3(playerInfo.x, playerInfo.y, playerInfo.z - distance);
	}
}
