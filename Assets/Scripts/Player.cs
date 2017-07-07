using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float acceleration = 10f;
	public bool infiniteAcceleration = true;
	public float topSpeedX = 50f;
    public float dashSpeed = 150f;
    public bool grounded = true;
	public float maxDashTime = 30f;
    public float jumpPower = 10f;

	public float minJumpMomentum = 3f;

	private Rigidbody2D rigidBody;
	private Animator animator;

    private DashState dashState;
    private float timeSinceDashStart;

	/**
	 * Tracks whether a jump was the cause of the player's airborne state
	 * which is useful for determining whether releasing the jump button 
	 * should cut short the player's vertical momentum.
	 * */

	private bool airborneFromJump = false;

	// Use this for initialization
	void Start () {
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		animator.SetFloat ("Player_Velocity_Horiz", Mathf.Abs(rigidBody.velocity.x));
        animator.SetBool("Player_Grounded", grounded);
        animator.SetBool("Player_Dashing", (dashState == DashState.DASHING));

		if (rigidBody.velocity.x < 0) {
			transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
		} else {
			transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
		}

        switch (dashState) {
			case DashState.READY:
				var isDashKeyDown = Input.GetKeyDown (KeyCode.RightShift);
				if (isDashKeyDown) {
					performDash ();
				} else {
					applyRun ();
					capVelocityX ();
				}
				break;
            case DashState.DASHING:
                timeSinceDashStart += Time.deltaTime * 60;
                if (timeSinceDashStart > maxDashTime)
                {
                    dashState = DashState.READY;
					applyRun ();
                }
                break;
		}

		truncateJump ();
	}

	void FixedUpdate() {
        if (Input.GetKeyDown("space") && grounded) {
            Vector3 up = transform.TransformDirection(Vector3.up);
            rigidBody.AddForce(up * jumpPower, ForceMode2D.Impulse);
			airborneFromJump = true;
        }
    }

    private void performDash() {
        var directionMultiplier = Input.GetAxis("Horizontal") >= 0 ? 1 : -1;
        dashState = DashState.DASHING;
		rigidBody.velocity = Vector2.right * dashSpeed * directionMultiplier;
        timeSinceDashStart = 0;
	}

	private void capVelocityX() {
		float currentHorizVelocity = rigidBody.velocity.x;
		if (Mathf.Abs(currentHorizVelocity) > topSpeedX) {
			int multiplier = currentHorizVelocity > 0 ? 1 : -1;
			rigidBody.velocity = new Vector2(topSpeedX * multiplier, rigidBody.velocity.y);
			Debug.Log("capping velocity");
		}
	}

	private void applyRun() {
		float horizInput = Input.GetAxis ("Horizontal");
		float currentHorizVelocity = rigidBody.velocity.x;

		if (horizInput != 0) {
			float force = (horizInput) * (infiniteAcceleration ? float.MaxValue : acceleration);
			rigidBody.AddForce (Vector2.right * force, ForceMode2D.Impulse);
		} else {
			rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
		}
	}

	private void truncateJump() {
		if (!airborneFromJump) {
			return;
		}

		if (!Input.GetKeyDown ("space")) {

		}
	}

	public void setGrounded(bool isGrounded) {
		grounded = isGrounded;
		airborneFromJump = !grounded && airborneFromJump;
	}

    public enum DashState {
        READY,
        DASHING
    }
}
