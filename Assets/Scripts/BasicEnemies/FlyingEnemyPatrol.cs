using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyPatrol : MonoBehaviour {

	Rigidbody2D enemyRB; 

	float moveXDir;
	public float flapHeight;
	
	[SerializeField]
	float flightMin; 
	float flightMax;
	
	float speed;  
	
	bool activeAI; 

	void Awake(){
		enemyRB = GetComponent<Rigidbody2D>(); 
		
		flightMin = transform.position.y+2f; 
		
		flapHeight = .8f;
		speed = 5f; 
		
		moveXDir = -1; 
		
		activeAI = false; 
	}
	
	void FixedUpdate(){
		if (transform.position.y <= flightMin){
			Flap();
		}

		if(activeAI){
			MoveEnemyX();
		}
	}
	
	public void ToggleAI(){
		activeAI = !activeAI;
	}
	
	void MoveEnemyX(){
		enemyRB.velocity = new Vector2(moveXDir*speed, enemyRB.velocity.y); 
	}
	
	void Flap(){
		print ("Flapped");
		enemyRB.AddForce(new Vector2(0f,1*flapHeight), ForceMode2D.Impulse); 
	}
	
	void FlipXDir(){
		moveXDir *= -1;
	}
	
	void OnTriggerExit2D (Collider2D col){
		if (col.gameObject.CompareTag("PatrolArea")){
			FlipXDir(); 
		}
	}
}
