using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraSystem : MonoBehaviour {

	private Vector2 velocity; 

	public GameObject player;
	
	public float smoothTimeX;
	public float smoothTimeY; 	
	
	public float zoom; 
	
	public float y; 
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		zoom = 6f;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float x = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		//float y = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
		transform.position = new Vector3 (x, y, transform.position.z);
		Camera.main.orthographicSize = zoom;
		
	}
}
