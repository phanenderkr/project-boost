using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour{
	private Rigidbody rigidBody;
	// Use this for initialization
	void Start (){
		rigidBody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update (){
		ProcessInput();
	}

	void ProcessInput(){
		//can thrust while rotating
		if (Input.GetKey(KeyCode.Space)){
			rigidBody.AddRelativeForce(Vector3.up * 1);
			print("thrusting");
		}

		if (Input.GetKey(KeyCode.A)){
			print("Roate left");
		} else if (Input.GetKey(KeyCode.D)){
			print("Roate left");
		}
	}
}
