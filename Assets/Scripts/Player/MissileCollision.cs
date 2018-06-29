using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollision : MonoBehaviour {

	float bulletDamage = 10f; 
	
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
