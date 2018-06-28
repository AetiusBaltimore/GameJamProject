using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HUDupdate : MonoBehaviour {

	PlayerStats ps;
	
	public float healthValue;
	public float energyValue;
	
	float healthBarFill;
	float energyBarFill;
	
	public float healthBarMax;
	public float energyBarMax;
	
	public Image healthBar;
	public Image energyBar; 
	
	
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
