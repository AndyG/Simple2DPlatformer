using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

    public float bounceForce;
    public float bounceLimit = -1;

    private GroundChecker groundChecker;
    private int environmentLayerMask;
    private Rigidbody2D rigidBody;
    private int bounces = 0;

    private void Start() {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        environmentLayerMask = 1 << LayerMask.NameToLayer("Environment");
        groundChecker = new GroundCheckerRays(gameObject.GetComponent<Collider2D>(), transform, environmentLayerMask);
    }

    void Update () {
        bool grounded = groundChecker.isGrounded();
        if (grounded && rigidBody.velocity.y <= 0) {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, bounceForce);
            bounces++;
            destroyIfPastLimit();
        }
	}

    private void destroyIfPastLimit() {
        if (bounceLimit >= 0 && bounces >= bounceLimit) {
            GameObject.Destroy(gameObject);
        }
    }
}
