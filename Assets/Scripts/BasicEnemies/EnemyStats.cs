using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	SpriteRenderer enemySR; 

	float health;
	
	void Awake(){
		enemySR = GetComponent<SpriteRenderer>(); 
		
		health = 2f; 
	}
	
	void Update(){
		if(gameObject.transform.position.y <= -5.5f){
			Destroy(gameObject);
		}
	}
	
	public void TakeDamage(float damage){
		health -= damage;
		print (gameObject.name+" took "+damage.ToString()+" and has "+health.ToString()+" left"); 
		
		if (health <= 0f){
			StartCoroutine("Die");
		}
	}
	
	IEnumerator Die(){
		health = 100f;
		enemySR.enabled = false;
		yield return new WaitForSeconds(.1f);
		enemySR.enabled = true;
		yield return new WaitForSeconds(.1f);
		enemySR.enabled = false;
		yield return new WaitForSeconds(.1f);
		enemySR.enabled = true;
		yield return new WaitForSeconds(.1f);
		Destroy(gameObject); 
	}
}
