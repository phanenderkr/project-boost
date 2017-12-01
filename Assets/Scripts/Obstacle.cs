using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Obstacle : MonoBehaviour{
	private Vector3 change;

	private bool up = true;
	// Use this for initialization
	void Start () {
		change = gameObject.transform.position;
		print(change);
	}
	
	// Update is called once per frame
	void Update (){
		if (gameObject.transform.position.y >= 28){
			up = false;
		}else if (gameObject.transform.position.y <= 10.8){
			up = true;
		}
		if (up){
			change.y += 0.1f;
		} else{
			change.y -= 0.1f;
		}
		
		gameObject.transform.position = change;
		print(change);
	}
}
