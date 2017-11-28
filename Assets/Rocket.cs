using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour{
	private Rigidbody rigidBody;
	private AudioSource audioSource;
	[SerializeField]
	float rcsThrust = 100f, thrust =1000f;
	// Use this for initialization
	void Start (){
		rigidBody = gameObject.GetComponent<Rigidbody>();
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update (){
		Thrust();
		Rotate();
	}

	void Rotate(){
		rigidBody.freezeRotation = true;
		
		float rotationSpeed = rcsThrust * Time.deltaTime;
		if (Input.GetKey(KeyCode.A)){
			transform.Rotate(Vector3.forward * rotationSpeed);
		} else if (Input.GetKey(KeyCode.D)){
			transform.Rotate(Vector3.back * rotationSpeed);
		}
		rigidBody.freezeRotation = false;
	}

	private void Thrust(){
		if (Input.GetKey(KeyCode.Space)){
			float thrustSpeed = thrust * Time.deltaTime;
//			print(thrust + ", " + Time.deltaTime);
			rigidBody.AddRelativeForce(Vector3.up * thrustSpeed);
			if (!audioSource.isPlaying){
				audioSource.Play();
			}
		}
		if (Input.GetKeyUp(KeyCode.Space)){
			audioSource.Stop();
		}
	}

	private void OnCollisionEnter(Collision other){
		switch (other.gameObject.tag){
			case "Friendly":
				print("Ok");
				break;
			default:
				print("Dead");
				break;
		}
	}
}
