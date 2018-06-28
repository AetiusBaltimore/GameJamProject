using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {

	public GameObject projectilePrefab; 
	
	PlayerMovement PM; 
		
	GameObject curBullet;
	Vector2 curBulletDir;
	Quaternion curBulletRot; 
	
	float projectileVelocity; 
	float aimY; 

	void Start () {
		PM = GetComponent<PlayerMovement>(); 
		
		projectileVelocity = 50000f; 
	}
	
	void Update () {
		if (Input.GetButtonDown("Shoot Bullet")){
			StartCoroutine("ShootBullet"); 
		}
	}
	
	void CreateBullet(){
		curBullet = (GameObject)Instantiate(projectilePrefab, transform.position, Quaternion.identity);
	}
	
	IEnumerator ShootBullet(){
		CreateBullet();
		CreateBulletDir();
		MoveBullet();
		yield return new WaitForSeconds(.2f); 
	}
	
	void MoveBullet(){
		Rigidbody2D bulletRB; 
			bulletRB = curBullet.GetComponent<Rigidbody2D>(); 
			curBullet.transform.rotation = curBulletRot;
			if (curBullet != null){ 
				bulletRB.AddForce(curBulletDir*Time.deltaTime*projectileVelocity); 
			}
	}
	
	void CreateBulletDir(){
		aimY = Input.GetAxis("Vertical");
		if(PM.FacingLeft){
			
			if (aimY >= .1f){
				curBulletDir = new Vector2(-1f,1f);
				curBulletRot = Quaternion.AngleAxis(-45f, Vector3.forward); 
			} else if (aimY <= -.1f){
				curBulletDir = new Vector2(-1f,-1f);
				curBulletRot = Quaternion.AngleAxis(45f, Vector3.forward);
			} else {
				curBulletDir = Vector2.left;
				curBulletRot = Quaternion.AngleAxis(0f, Vector3.forward);
			}
		} else { 
			if (aimY >= .1f){
				curBulletDir = new Vector2(1f, 1f);
				curBulletRot = Quaternion.AngleAxis(45f, Vector3.forward);
			} else if (aimY <= -.1f){
				curBulletDir = new Vector2(1f,-1f);
				curBulletRot = Quaternion.AngleAxis(-45f, Vector3.forward);
			} else {
				curBulletDir = Vector2.right;
				curBulletRot = Quaternion.AngleAxis(0f, Vector3.forward);
			}
		}
	}
}
