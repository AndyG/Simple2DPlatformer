using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 50f;
    public float dashPower = 150f;
    public bool grounded = true;
    public float maxDashTime;
    public float jumpPower = 10;

	private Rigidbody2D rigidBody;
	private Animator animator;

    private DashState dashState;
    private float timeSinceDashStart;

	// Use this for initialization
	void Start () {
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat ("Player_Velocity_Horiz", Mathf.Abs(rigidBody.velocity.x));
        animator.SetBool("Player_Grounded", grounded);

        switch (dashState)
        {
            case DashState.READY:
                var isDashKeyDown = Input.GetKeyDown(KeyCode.RightShift);
                if (isDashKeyDown)
                {
                    performDash();
                } else
                {
                    float horizForce = Input.GetAxis("Horizontal");
                    rigidBody.velocity = new Vector2(speed * horizForce, rigidBody.velocity.y);
                }
                break;
            case DashState.DASHING:
                timeSinceDashStart += Time.deltaTime * 60;
                Debug.Log("timeSinceDashStart: " + timeSinceDashStart);
                if (timeSinceDashStart > maxDashTime)
                {
                    dashState = DashState.READY;
                    rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                }
                break;
        }
    }

	void FixedUpdate() {
        if (Input.GetKeyDown("space") && grounded) {
            Vector3 up = transform.TransformDirection(Vector3.up);
            rigidBody.AddForce(up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void performDash()
    {
        var directionMultiplier = Input.GetAxis("Horizontal") >= 0 ? 1 : -1;
        dashState = DashState.DASHING;
        rigidBody.velocity = Vector2.right * dashPower * directionMultiplier;
        rigidBody.angularVelocity = 0;
        rigidBody.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        timeSinceDashStart = 0;
    }

    public enum DashState
    {
        READY,
        DASHING
    }
}
