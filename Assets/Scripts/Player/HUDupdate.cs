using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HUDupdate : MonoBehaviour {

	PlayerStats ps;
	
	float healthValue;
	float energyValue;
	
	float healthBarFill;
	float energyBarFill;
	
	public float healthBarMax;
	public float energyBarMax;
	
	public Image healthBar;
	public Image energyBar; 
	
	public float HealthValue{
		get{return healthValue;}
		set{healthValue = value;}
	}
	
	public float EnergyValue{
		get{return energyValue;}
		set{energyValue = value;}
	}
	
	void Start(){
		ps = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
	
		healthBar.fillAmount = 1f;
		energyBar.fillAmount = 1f;
	}
	
	void Update(){
		UpdateHPBar();
		UpdateEBar(); 
	}
	
	void UpdateHPBar(){
		healthValue = (ps.Health/healthBarMax);
		healthBar.fillAmount = healthValue; 
	}
	
	void UpdateEBar(){
		energyValue = (ps.Energy/energyBarMax);
		energyBar.fillAmount = energyValue; 
	}
}
