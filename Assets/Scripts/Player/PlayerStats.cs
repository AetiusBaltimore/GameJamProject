using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerStats : MonoBehaviour {
	
	HUDupdate HUD; 
	
	[SerializeField]
	float health;
	[SerializeField]
	float energy;
	
	float healthMax;
	float energyMax; 
	
	float energyRegenValue;
	float energyRegenRate;
	bool regenTick;
	
	public static bool PlayerExists; 
	
	public float Health{
		get {return health;}
	}
	public float Energy{
		get {return energy;}
	}
	public float HealthMax{
		get {return healthMax;}
	}
	public float EnergyMax{
		get {return energyMax;}
	}
	
	void Start () {
		PlayerExists = true; 
		
		HUD = GameObject.FindWithTag("HUD").GetComponent<HUDupdate>(); 
		
		healthMax = 10f;
		health = healthMax; 
		HUD.healthBarMax = healthMax;
		
		energyMax = 100f;
		energy = energyMax;
		HUD.energyBarMax = energyMax; 
		energyRegenRate = 1f;
		energyRegenValue = 1f; 
		
		regenTick = true; 
	}
	
	void Update () {
		if (gameObject.transform.position.y <= -5.5f || health <= 0){
			StartCoroutine("Die");
		}
		
		if ((energy < 100f) && regenTick){
			StartCoroutine("RegenEnergy"); 
		}
	}
	
	IEnumerator Die(){
		yield return new WaitForSeconds(.1f);
		Scene curScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(curScene.name);
	}
	
	public void DamageHealth(){
		float damage = 1f; 
		health -= damage; 
		HUD.HealthValue = health;
	}
	
	public void DrainEnergy(float energyCost){
		energy -= energyCost;
		HUD.EnergyValue = energy; 
	}
	
	IEnumerator RegenEnergy(){
		regenTick = false; 
		energy += energyRegenValue;
		yield return new WaitForSeconds(energyRegenRate); 
		regenTick = true; 
	}
	
	public void IncreaseHealth(float restoreValue){
		health += restoreValue;
		HUD.HealthValue = health; 
	}
	
	public void IncreaseEnergy(float restoreValue){
		energy += restoreValue;
		HUD.HealthValue = energy; 
	}
	
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.CompareTag("Enemy")){
			DamageHealth(); 
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.CompareTag("SmallHealthPickup")){
			IncreaseHealth(2f); 
			Destroy(col.gameObject); 
		}
	}
}
