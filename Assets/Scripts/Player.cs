using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float acceleration = 10f;
	public bool infiniteAcceleration = true;
	public float topSpeedX = 50f;
    public float dashSpeed = 150f;
	public float maxDashTime = 30f;
    public float jumpPower = 10f;
	public float minJumpMomentum = 4f;
    public float postDashVelocityY = -5f;
    public int maxAirdashesPerAirborne = 1;
	public float jumpLeniency = 60f;
    public float groundDetectionDistance = 5f;

	private Rigidbody2D rigidBody;
	private Animator animator;

    private SpriteFlipper spriteFlipper;
    private GroundChecker groundChecker;
    private WallPushChecker wallPushChecker;

    private DashState dashState;
    private WallPushState wallPushState;
    private float timeSinceDashStart;

    private int airdashesSinceAirborne = 0;
	private float timeSinceGrounded = 0f;

    private bool grounded = true;

    private float intrinsicGravity;
    private int environmentLayerMask;

	void Start () {
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
        intrinsicGravity = rigidBody.gravityScale;

		animator = gameObject.GetComponent<Animator> ();
        environmentLayerMask = 1 << LayerMask.NameToLayer("Environment");

        spriteFlipper = new SpriteFlipper(transform);
        groundChecker = new GroundCheckerRays(gameObject.GetComponent<Collider2D>(), transform, environmentLayerMask);
        wallPushChecker = new WallPushCheckerRays(gameObject.GetComponent<Collider2D>(), transform, environmentLayerMask);
    }
	
	void Update () {

        grounded = groundChecker.isGrounded();
        wallPushState = wallPushChecker.getWallPushState();

		if (grounded) {
			timeSinceGrounded = 0f;
            airdashesSinceAirborne = 0;
		} else {
			timeSinceGrounded += Time.deltaTime;
		}

		animator.SetFloat ("Player_Velocity_Horiz", Mathf.Abs(rigidBody.velocity.x));
        animator.SetBool("Player_Grounded", grounded);
        animator.SetBool("Player_Dashing", (dashState == DashState.DASHING));
        animator.SetBool("Player_Pushing_Wall", (grounded && (wallPushState != WallPushState.NONE)));

        switch (dashState) {
			case DashState.READY:
                rigidBody.gravityScale = intrinsicGravity;
				if (!performDash()) {
                    performHorizMove();
				}
				break;
            case DashState.DASHING:
                timeSinceDashStart += Time.deltaTime * 60;
                if (timeSinceDashStart > maxDashTime) {
                    dashState = DashState.READY;
                    setVelocity(0, postDashVelocityY);
                    performHorizMove();
                }
                break;
		}

        processJump();
        flipSpriteIfGoingLeft();
	}

    // Returns true if a dash was performed.
    private bool performDash() {
        if (!grounded && airdashesSinceAirborne >= maxAirdashesPerAirborne) {
            return false;
        }

        bool isDashKeyDown = Input.GetKeyDown(KeyCode.RightShift);
        if (isDashKeyDown) {
            dashState = DashState.DASHING;
            rigidBody.gravityScale = 0f;
            var directionMultiplier = Input.GetAxis("Horizontal") >= 0 ? 1 : -1;
            setVelocity(dashSpeed * directionMultiplier, 0);
            timeSinceDashStart = 0;

            if (!grounded) {
                airdashesSinceAirborne++;
            }
            return true;
        }

        return false;
	}
	
	private void performHorizMove() {
		float horizInput = Input.GetAxis ("Horizontal");

        if (wallPushState == WallPushState.PUSHING_LEFT && horizInput < 0) {
            return;
        }
         
        if (wallPushState == WallPushState.PUSHING_RIGHT && horizInput > 0) {
            return;
        }

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
		bool canJump = grounded || (timeSinceGrounded < jumpLeniency);

		if (Input.GetKeyDown("space") && canJump)
        {
            Vector3 up = transform.TransformDirection(Vector3.up);
            rigidBody.AddForce(up * jumpPower, ForceMode2D.Impulse);
            dashState = DashState.READY;
        }

        tryShortenJump();
    }

	private void tryShortenJump() {
		if (Input.GetKeyUp ("space") && rigidBody.velocity.y > minJumpMomentum) {
            setVelocityY(minJumpMomentum);
		}
	}

    private void flipSpriteIfGoingLeft() {
        spriteFlipper.flipSprite(rigidBody.velocity.x < 0 || wallPushState == WallPushState.PUSHING_LEFT);
    }

    /**
     * Various helper methods for updating position or velocity. 
     */
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

    private enum DashState {
        READY,
        DASHING
    }
}
