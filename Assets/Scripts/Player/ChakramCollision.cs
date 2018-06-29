using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakramCollision : MonoBehaviour {

	DistanceJoint2D distanceJoint; 
	
	Rigidbody2D playerRB; 

	float bulletDamage = 5f; 
	
	void Awake(){
		distanceJoint = GetComponent<DistanceJoint2D>(); 
		playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>(); 

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
		
		if (col.gameObject.CompareTag("Shield")){
			Destroy(col.gameObject); 
			Destroy(gameObject); 
		}
	}
}
