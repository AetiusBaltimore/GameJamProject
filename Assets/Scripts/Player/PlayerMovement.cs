using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	SpriteRenderer playerSR;
	Rigidbody2D playerRB;
	Animator anim; 
	
	[SerializeField]
	bool canMove; 
	bool playerMoving; 
	float moveX;
	float speed; 

	public float dashSpeed; 
	[SerializeField]
	bool canDash; 
	public float dashDur;
	public float dashCD; 
	
	public float airSpeed; 
	public float jumpHeight; 

	bool jumping;
	bool dashing;
	
	bool facingLeft;
	//bool playerFalling; 
	bool playerGrounded;
	
	public bool FacingLeft{
		get {return facingLeft;}
	}
	
	void Start () {
		playerSR = GetComponent<SpriteRenderer>(); 
		playerRB = GetComponent<Rigidbody2D>(); 
		//anim = GetComponent<Animator>(); 
		
		speed = 12f;
		airSpeed = 100f;
		jumpHeight = 1000f; 
		dashSpeed = 30f; 
		dashCD = .3f;
		dashDur = .3f; 
		
		facingLeft = false;
		canMove = true; 
		canDash = true;

		jumping = false;
		dashing = false; 
	}
	
	void Update(){
		if (Input.GetButtonDown("Jump") && (playerGrounded)) {
			jumping = true;
		}
		if (Input.GetButtonDown("Dash") && (canDash)){
			dashing = true;
		} 
	}
	
	void FixedUpdate () {
		//CheckPlayerFalling(); 		
		if (dashing){
			StartCoroutine("Dash");
		}
		
		if (jumping) { 
				Jump(); 
			}
		
		if (canMove){
			PlayerMove(); 
		}
	}
	
	void PlayerMove(){
		moveX = Input.GetAxis("Horizontal");
		if (moveX <= -.01f){
			if (!facingLeft){
				facingLeft = !facingLeft; 
				SetFace(); 
			}
		} else if (moveX >= .01f){
			if (facingLeft){
				facingLeft = !facingLeft; 
				SetFace(); 
			}
		}
		playerRB.velocity = new Vector2 (moveX*speed, playerRB.velocity.y); 
	}
	
	void SetFace(){
		playerSR.flipX = facingLeft; 
	}
	
	IEnumerator Dash(){
		dashing = false; 
		canDash = false; 
		canMove = false; 
		float time = 0f; 
		
		while (dashDur > time){
			if (Input.GetButtonDown("Jump")&& !jumping){
				//airspeed = 400000f;
				jumpHeight = 5000000f;
				DashJump();
				jumpHeight = 1000f; 
				canMove = true;
				canDash = true; 
				//airspeed = 100f; 
				//jumpHeight = 150f; 
				yield break; 
			}
			time += Time.deltaTime; 
			if (facingLeft){
				playerRB.AddForce(new Vector2(-1*dashSpeed,playerRB.velocity.y)); 
			} else {
				playerRB.AddForce(new Vector2(1*dashSpeed,playerRB.velocity.y)); 
			}
			yield return 0; 
		}
		canMove = true; 
		yield return new WaitForSeconds(dashCD); 
		canDash = true; 
	}
	
	void Jump(){
		jumping = false;
		playerRB.AddForce(new Vector2(playerRB.velocity.x*airSpeed,1*jumpHeight)*Time.deltaTime, ForceMode2D.Impulse); 	
		playerGrounded = false; 
	}
	
	void DashJump(){
		jumping = false;
		playerRB.AddForce(new Vector2(playerRB.velocity.x*airSpeed,1*jumpHeight), ForceMode2D.Impulse); 
		playerGrounded = false; 
	}
	
	// void CheckPlayerFalling(){
		// bool falling; 
		// if (playerRB.velocity.y <= -.1f){
			// falling = true; 
		// } else {
			// falling = false;  
		// } 
		
		// if (falling != playerFalling){
			// anim.SetBool("Falling", falling);
		// }
	// }
	
	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.CompareTag("Ground")){
			playerGrounded = true; 
		}
		if (col.gameObject.CompareTag("Enemy")){
			playerGrounded = true; 
		}
	}
}
