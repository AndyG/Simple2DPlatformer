using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float acceleration = 10f;
	public bool infiniteAcceleration = true;
	public float topSpeedX = 50f;
    public float dashSpeed = 150f;
    private bool grounded = true;
	public float maxDashTime = 30f;
    public float jumpPower = 10f;

	public float minJumpMomentum = 4f;
    public float postDashVelocityY = -5f;

	private Rigidbody2D rigidBody;
	private Animator animator;

    private DashState dashState;
    private float timeSinceDashStart;

    private float intrinsicGravity;

	// Use this for initialization
	void Start () {
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
        intrinsicGravity = rigidBody.gravityScale;

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
                rigidBody.gravityScale = intrinsicGravity;
				var isDashKeyDown = Input.GetKeyDown (KeyCode.RightShift);
				if (isDashKeyDown) {
					performDash ();
				} else {
					applyRun ();
				}
				break;
            case DashState.DASHING:
                rigidBody.gravityScale = 0f;
                timeSinceDashStart += Time.deltaTime * 60;
                if (timeSinceDashStart > maxDashTime) {
                    dashState = DashState.READY;
                    setVelocity(0, postDashVelocityY);
                    applyRun ();
                }
                break;
		}

        processJump();
	}

    private void performDash() {
        dashState = DashState.DASHING;
        var directionMultiplier = Input.GetAxis("Horizontal") >= 0 ? 1 : -1;
		setVelocity(dashSpeed * directionMultiplier, 0);
        timeSinceDashStart = 0;
	}
	
	private void applyRun() {
		float horizInput = Input.GetAxis ("Horizontal");

		if (horizInput != 0) {
			float force = (horizInput) * (infiniteAcceleration ? float.MaxValue : acceleration);
			rigidBody.AddForce (Vector2.right * force, ForceMode2D.Impulse);
            capVelocityX();
        } else {
            setVelocityX(0);
		}
    }

    private void capVelocityX() {
        float currentHorizVelocity = rigidBody.velocity.x;
        if (Mathf.Abs(currentHorizVelocity) > topSpeedX)
        {
            int multiplier = currentHorizVelocity > 0 ? 1 : -1;
            setVelocityX(topSpeedX * multiplier);
        }
    }

    private void processJump() {
        if (Input.GetKeyDown("space") && grounded)
        {
            Vector3 up = transform.TransformDirection(Vector3.up);
            rigidBody.AddForce(up * jumpPower, ForceMode2D.Impulse);
        }

        if (!grounded)
        {
            truncateJump();
        }
    }

	private void truncateJump() {
		if (Input.GetKeyUp ("space") && rigidBody.velocity.y > minJumpMomentum) {
            setVelocityY(minJumpMomentum);
		}
	}

    private void setVelocityX(float x) {
        setVelocity(x, rigidBody.velocity.y);
    }

    private void setVelocityY(float y)
    {
        setVelocity(rigidBody.velocity.x, y);
    }

    private void setVelocity(float x, float y)
    {
        rigidBody.velocity = new Vector2(x, y);
    }

	public void setGrounded(bool isGrounded) {
		grounded = isGrounded;
	}

    public enum DashState {
        READY,
        DASHING
    }
}
