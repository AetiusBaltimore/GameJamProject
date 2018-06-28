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
	float playerSpeed; 

	public float dashSpeed; 
	[SerializeField]
	bool canDash; 
	public float dashDur;
	public float dashCD; 
	
	float playerAirSpeed; 
	float playerJumpHeight; 

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
		
		playerSpeed = 12f;
		playerAirSpeed = 100f;
		playerJumpHeight = 150f; 
		dashSpeed = 30f; 
		dashCD = .3f;
		dashDur = .3f; 
		
		facingLeft = false;
		canMove = true; 
		canDash = true; 
	}
	
	void FixedUpdate () {
		//CheckPlayerFalling(); 
		if(gameObject.transform.position.y <= -5.5f){
			//die
			return;
		}
		
		if (Input.GetButtonDown("Dash") && (canDash)){
			StartCoroutine("Dash");
		}
		
		if (Input.GetButtonDown("Jump") && (playerGrounded)) {
				print ("Player pressed jump"); 
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
		playerRB.velocity = new Vector2 (moveX*playerSpeed, playerRB.velocity.y); 
	}
	
	void SetFace(){
		playerSR.flipX = facingLeft; 
	}
	
	IEnumerator Dash(){
		print ("Dashed"); 
		canDash = false; 
		canMove = false; 
		float time = 0f; 
		
		while (dashDur > time){
			time += Time.deltaTime; 
			if (facingLeft){
				playerRB.velocity = Vector2.left*dashSpeed; 
			} else {
				playerRB.velocity = Vector2.right*dashSpeed; 
			}
			yield return 0; 
		}
		canMove = true; 
		yield return new WaitForSeconds(dashCD); 
		canDash = true; 
	}
	
	void Jump(){
		print ("Player jumped");
		//bool jumping = true; 
		//if (!jumping){
			if (playerGrounded){
				if (moveX > 0.1f){
					playerRB.AddForce (Vector2.up * playerJumpHeight, ForceMode2D.Impulse);
					playerRB.AddForce (Vector2.right * playerAirSpeed, ForceMode2D.Impulse);
					playerGrounded = false;
				}else if (moveX < -0.1f){
					playerRB.AddForce (Vector2.up * playerJumpHeight, ForceMode2D.Impulse);
					playerRB.AddForce (Vector2.left * playerAirSpeed, ForceMode2D.Impulse);
					playerGrounded = false;
				} else {
					playerRB.AddForce (Vector2.up * playerJumpHeight, ForceMode2D.Impulse);
					playerGrounded = false;
				}
				//yield return new WaitForSeconds(.2f); 
				//canAirJump = true;
				//jumping = false; 
			}
		//}
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
	}
}
