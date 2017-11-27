using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour{
	private Rigidbody rigidBody;
	private AudioSource audioSource;
	
	// Use this for initialization
	void Start (){
		rigidBody = gameObject.GetComponent<Rigidbody>();
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update (){
		ProcessInput();
	}

	void ProcessInput(){
		//can thrust while rotating
		if (Input.GetKey(KeyCode.Space)){
			rigidBody.AddRelativeForce(Vector3.up * 1);
			if (!audioSource.isPlaying){
				audioSource.Play();
			}	
		}
		if (Input.GetKeyUp(KeyCode.Space)){
			audioSource.Stop();
		}

		if (Input.GetKey(KeyCode.A)){
			transform.Rotate(Vector3.forward);
			print("Roate left");
		} else if (Input.GetKey(KeyCode.D)){
			transform.Rotate(Vector3.back);
			print("Roate left");
		}
	}
}
