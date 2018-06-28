using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerStats : MonoBehaviour {
	
	HUDupdate HUD; 
	
	[SerializeField]
	float health;
	float energy;
	
	float healthMax;
	float energyMax; 
	
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
		HUD = GameObject.FindWithTag("HUD").GetComponent<HUDupdate>(); 
		
		healthMax = 10f;
		health = healthMax; 
		HUD.healthBarMax = healthMax;
		
		energyMax = 100f;
		energy = energyMax;
		HUD.energyBarMax = energyMax; 
	}
	
	void Update () {
		if (gameObject.transform.position.y <= -5.5f || health <= 0){
			StartCoroutine("Die");
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
		HUD.healthValue = health;
	}
}
