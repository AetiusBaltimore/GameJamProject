using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {

	Rigidbody2D enemyRB;

	public float moveXDir; 
	public float speed; 
	
	[SerializeField]
	bool activeAI;
	
	void Awake(){
		enemyRB = GetComponent<Rigidbody2D>(); 
		
		activeAI = false;
		
		moveXDir = -1; 
		speed = 5; 
	}
	
	void FixedUpdate(){
		if(activeAI){
			MoveEnemy();
		}
	}
	
	public void ToggleAI(){
		activeAI = !activeAI; 
	}
	
	void MoveEnemy(){
		enemyRB.velocity = (new Vector2(moveXDir*speed, enemyRB.velocity.y)); 
	}
	
	void FlipXDir(){
		moveXDir *= -1; 
	}
	
	void StopMovement(){
		moveXDir = 0;
	}
	
	void SetLeft(){
		moveXDir = -1;
	}	
	
	void SetRight(){
		moveXDir = 1; 
	}
	
	void OnCollisionEnter2D (Collision2D col){
		if(col.gameObject.CompareTag("Player")){
			FlipXDir(); 
		}
		if (col.gameObject.CompareTag("Ground")){
			FlipXDir(); 
		}
	}
}
