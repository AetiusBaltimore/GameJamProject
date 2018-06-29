using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakramCollision : MonoBehaviour {

	DistanceJoint2D distanceJoint; 
	
	Rigidbody2D bulletRB; 
	
	GameObject playerGO;
	Rigidbody2D playerRB; 
	PlayerMovement PM; 
	
	float bulletDamage = 5f; 
	
	void Awake(){
		distanceJoint = GetComponent<DistanceJoint2D>(); 

		bulletRB = GetComponent<Rigidbody2D>(); 
		
		playerGO = GameObject.FindWithTag("Player"); 
		playerRB = playerGO.GetComponent<Rigidbody2D>();
		PM = playerGO.GetComponent<PlayerMovement>(); 
		
		
		distanceJoint.enabled = false; 

	}
	
	void Update(){
		Vector3 bulletScreenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position); 
		if (bulletScreenPos.y >= Screen.height || bulletScreenPos.y <= 0 || bulletScreenPos.x >= Screen.width || bulletScreenPos.x <= 0){
			Destroy(gameObject); 
		}
	}
	
	void OnTriggerEnter2D (Collider2D col){
		print (col);
		
		if (col.gameObject.CompareTag("Enemy")){
			print ("Hit enemy"); 
			col.SendMessageUpwards("TakeDamage", bulletDamage, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject); 
		}
		
		if (col.gameObject.CompareTag("Ground")){
			Destroy(gameObject); 
		}
		
		if (col.gameObject.CompareTag("Ceiling")){
			ConnectPlayer();
		}
		
		if (col.gameObject.CompareTag("Shield")){
			Destroy(col.gameObject); 
			Destroy(gameObject); 
		}
	}
	
	void ConnectPlayer(){
		bulletRB.velocity = Vector2.zero;
		bulletRB.bodyType = RigidbodyType2D.Static;
		distanceJoint.enabled = true; 
		distanceJoint.connectedBody = playerRB; 
		playerRB.gravityScale = 80f;
		PM.ConnectedToGrapple = true; 
	}
	
	public void DisconnectPlayer(){
		distanceJoint.enabled = false;
		playerRB.gravityScale = 5f;
		Destroy(gameObject); 
	}
}
