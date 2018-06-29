using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {

	public GameObject bulletPrefab; 
	public GameObject missilePrefab;
	public GameObject chakramPrefab; 
	
	PlayerMovement PM; 
	PlayerStats PS; 	
	
	GameObject curBullet;
	Vector2 curBulletDir;
	Quaternion curBulletRot; 
	
	Rigidbody2D bulletRB;
	
	float projectileVelocity; 
	float aimY; 
	
	float bulletCD;
	float missileCD;
	float chakramCD; 
	
	bool canShootBullet;
	bool canShootMissile; 
	bool canShootChakram;
	
	float missileEnergyCost; 
	float chakramEnergyCost;

	public bool CanShootChakram{
		set{canShootChakram = value;}
	}
	
	void Start () {
		PM = GetComponent<PlayerMovement>(); 
		PS = GetComponent<PlayerStats>(); 
		
		projectileVelocity = 50000f;

		bulletCD = .2f; 
		missileCD = 1f;
		chakramCD = .5f;

		canShootBullet = true; 
		canShootMissile = true;
		canShootChakram = true;
		
		missileEnergyCost = 10f;
		chakramEnergyCost = 20f; 
	}
	
	void Update () {
		if (Input.GetButtonDown("Shoot Bullet") && canShootBullet){
			StartCoroutine("ShootBullet"); 
		}
		
		if (Input.GetButtonDown("Shoot Missile")){
			if (PS.Energy>=missileEnergyCost){
				if (canShootMissile){
					StartCoroutine("ShootMissile");
				} else {
					print("Missile still cooling down!");
				}
			} else {
				print("Not enough energy!"); 
			}
		}
		
		if (Input.GetButtonDown("Shoot Chakram")){
			if (PS.Energy>=chakramEnergyCost){
				if (canShootChakram){
					StartCoroutine("ShootChakram"); 
				} else {
					print("Chakram still cooling down!");
				}
			} else {
				print("Not enough energy!"); 
			}
		}
	}
	
	void CreateBullet(){
		curBullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.identity);
	}
	
	void CreateMissile(){
		curBullet = (GameObject)Instantiate(missilePrefab, transform.position, Quaternion.identity); 
	}
	
	void CreateChakram(){
		curBullet = (GameObject)Instantiate(chakramPrefab, transform.position, Quaternion.identity); 
	}
	
	IEnumerator ShootBullet(){
		canShootBullet = false; 
		CreateBullet();
		CreateBulletDir();
		MoveBullet();
		yield return new WaitForSeconds(bulletCD);
		canShootBullet = true; 
	}
	
	IEnumerator ShootMissile(){
		PS.DrainEnergy(missileEnergyCost); 
		canShootMissile = false;
		CreateMissile(); 
		CreateBulletDir();
		MoveBullet();
		yield return new WaitForSeconds(missileCD); 
		canShootMissile = true;			
	}
	
	IEnumerator ShootChakram(){
		PS.DrainEnergy(chakramEnergyCost); 
		canShootChakram = false;
		CreateChakram();
		CreateBulletDir();
		//projectileVelocity *= .7f;
		MoveBullet(); 
		yield return new WaitForSeconds(chakramCD);
		if(!PM.ConnectedToGrapple){
			canShootChakram = true; 
		}
	}
	
	void MoveBullet(){
		bulletRB = curBullet.GetComponent<Rigidbody2D>(); 
		curBullet.transform.rotation = curBulletRot;
		if (curBullet != null){ 
			bulletRB.AddForce(curBulletDir*Time.deltaTime*projectileVelocity); 
		}
	}
	
	void CreateBulletDir(){
		aimY = Input.GetAxisRaw("Vertical");
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
