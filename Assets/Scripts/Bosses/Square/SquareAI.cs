using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareAI : MonoBehaviour {

	bool updateState; 

	public enum AIState{
		
	}
	
	void Start(){
		updateState = true; 
	}
	
	void Update(){
		if (updateState){
			StartCoroutine("MakeDecision"); 
			
		}
	}
}
