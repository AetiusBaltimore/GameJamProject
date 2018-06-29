using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseScript : MonoBehaviour {

	GameObject player;
	//PlayerStats PS; 

	public static bool gamePaused = false; 
	//public TransitionManager transitionManager; 
	
	public GameObject pauseMenu; 
	
	void Awake(){
		player = GameObject.FindWithTag("Player");
		//PS = player.GetComponent<PlayerStats>(); 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Cancel")){
			if(gamePaused){
				Resume();
			} else {
				Pause(); 
			}
		}
	}
	
	public void Pause(){
		pauseMenu.SetActive(true); 
		Time.timeScale = 0f; 
		gamePaused = true; 
	}
	
	public void Resume(){
		pauseMenu.SetActive(false);
		Time.timeScale = 1f; 
		gamePaused = false; 
	}
	
	public void QuitToMenu(){
		Time.timeScale = 1f;
		gamePaused = false;
		//transitionManager.LoadScene("MainMenu"); 
	}
	
	public void QuitBattle(){
		Time.timeScale = 1f;
		gamePaused = false; 
		//transitionManager.LoadScene("Test"); 
	}
	
	public void QuitGame(){
		Time.timeScale = 1f;
		gamePaused = false;
		Application.Quit(); 
	}
	
	public void LoadBossScene(){			
		SceneManager.LoadScene("TestBossBattle"); 
	}
	
	public void ButtonClicked(){
		print("Button Clicked"); 
	}
	
	public void LoadTestArea(){
		SceneManager.LoadScene("TestScene"); 
	}
	
}
