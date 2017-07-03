using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 50f;
    public bool grounded = true;

	private Rigidbody2D rigidBody;
	private Animator animator;

	// Use this for initialization
	void Start () {
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat ("Player_Velocity_Horiz", Mathf.Abs(rigidBody.velocity.x));
        animator.SetBool("Player_Grounded", grounded);
    }

	void FixedUpdate() {
		float horizForce = Input.GetAxis("Horizontal");
		rigidBody.AddForce (Vector2.right * speed * horizForce);     

        if (Input.GetKeyDown("space") && grounded) {
            Vector3 up = transform.TransformDirection(Vector3.up);
            rigidBody.AddForce(up * 5, ForceMode2D.Impulse);
        }
    }
}
