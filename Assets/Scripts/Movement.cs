using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public bool canMove; //Can the player move ? 

	public float h = 0f; //Cached value of input - x
	public float v = 0f; //cached value of input - y

	public float maxSpeedX; 
	public float maxSpeedY; 
	public float currentSpeedX;
	public float currentSpeedY;
	public float accelerationX; 
	public float accelerationY; 
	public float dragDownY;
	public float jumpTime;
	public float currentJumpHeight;
	public float jumpFrequency;


	private bool isFalling;
	private bool grounded;
	private bool facingRight;
	private bool canJump;



	private Rigidbody2D rb;
	private GameObject groundCheck;
	//private FrictionJoint2D fj;
	private Animator anim;

	public bool Flip(bool facingRight)
	{
		if (facingRight)
			transform.localScale = new Vector3(-3, 3, 6);
		else transform.localScale = new Vector3(3, 3, 6);

		facingRight = !facingRight;
		return facingRight;
	}

	public bool IsGrounded()
	{
		return Physics2D.Raycast (groundCheck.transform.position, Vector2.down, 0.01f);
	}

	public bool IsFalling(bool grounded, bool isFalling)
	{
		if (!grounded && isFalling) 
		{
			return true;
		}
		return (!grounded && (rb.velocity.y < 0.01f));
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		isFalling = false;
		canJump = true;
		currentSpeedY = 0f;
		jumpFrequency = 0.1f;
	}


	void Awake()
	{

		rb = GetComponent<Rigidbody2D>();
		//fj = GetComponent<FrictionJoint2D>();
		anim = GetComponent<Animator> ();
		groundCheck = GameObject.Find ("groundCheck");
		anim.speed = 0.7f;
		grounded = false;
		isFalling = false;
		facingRight = true;
		canJump = true;
		maxSpeedX = 10f; 
		maxSpeedY = 9.9f; 
		currentSpeedX = 0f;
		currentSpeedY = 0f;
		accelerationX = 10f; 
		accelerationY = 1.65f; 
		dragDownY = 10f;
		jumpTime = 0.3f;
		jumpFrequency = 0.1f;
	}


	// Use this for initialization

	void Update()
	{

		grounded = IsGrounded ();
		isFalling = IsFalling(grounded, isFalling);

        if (!Input.anyKey && rb.velocity.magnitude == 0f && grounded)
            anim.Play("Idle");
        else if (rb.velocity.magnitude > 0f && grounded && !isFalling)
            anim.Play("Running");
        else if (rb.velocity.magnitude > 0f && !grounded && !isFalling)
        {
            //anim.SetTrigger("Jump");
            anim.Play("Jump");
        }
        else if (rb.velocity.magnitude > 0f && isFalling && !Input.anyKey && !anim.GetCurrentAnimatorStateInfo(0).IsName("MidAirSword"))
            anim.Play("Falling");
		

		 

	}


	// Update is called once per frame
	void FixedUpdate () 
	{
		
		jumpFrequency -= Time.deltaTime;
		h = Input.GetAxis ("Horizontal"); 
		v = Input.GetAxis ("Vertical");
		RaycastHit2D hit = Physics2D.Raycast (groundCheck.transform.position, -Vector2.up);
		if (hit.collider != null) 
		{
			currentJumpHeight = hit.distance;
		}



		if (Input.GetKey ("right") && !Input.GetKey("left ctrl")) 
		{
			if (!facingRight)
				facingRight = Flip (facingRight);
			rb.velocity = new Vector2 (currentSpeedX, rb.velocity.y);
			if (currentSpeedX < maxSpeedX) {
				currentSpeedX += accelerationX;
			}

		} else if (Input.GetKey ("left") && !Input.GetKey("left ctrl")) 
		{
			if (facingRight)
				facingRight = Flip (facingRight);
			rb.velocity = new Vector2 (-currentSpeedX, rb.velocity.y);
			if (currentSpeedX < maxSpeedX) {
				currentSpeedX += accelerationX;
			}
		} else if (!Input.GetKey ("right") || Input.GetKey ("left")) 
		{
                currentSpeedX = 0f;
                rb.velocity = new Vector2(currentSpeedX, rb.velocity.y);
			

		}

		if (Input.GetKey ("up") && canJump && jumpFrequency < 0f) {
			jumpTime -= Time.deltaTime;
			if (currentSpeedY < maxSpeedY) {
				currentSpeedY += accelerationY;
			} 
			if (jumpTime > 0) {
				rb.velocity = new Vector2 (rb.velocity.x, currentSpeedY); 
			} else 
			{
				canJump = false; 
				currentSpeedY = 0f;
			}
				
		} else if (jumpTime != 0.3f || (jumpTime == 0.3f && !grounded)) 
		{
			if (currentJumpHeight < 1.0f)
				rb.velocity = new Vector2 (rb.velocity.x, -6.6f);
			else 
				rb.velocity = new Vector2 (rb.velocity.x, -currentSpeedY);
			if (currentSpeedY < maxSpeedY) {
				currentSpeedY += accelerationY;
			} 
			jumpTime = 0.3f; 
			canJump = false;
		}
	}
}
