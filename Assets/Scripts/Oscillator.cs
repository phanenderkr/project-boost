using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour{
	[SerializeField] private Vector3 movementVector = new Vector3(10f,10f,10f);
	[SerializeField] private float period = 2f;
	
	private float movementFactor; //0 for not moved, 1 for fully moved

	private Vector3 startPosition;
	// Use this for initialization
	void Start (){
		startPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update (){
		if (period != 0){
			float cycles = Time.time / period;

			const float tau = (float) Math.PI * 2f;
			float rawSineWave = Mathf.Sin(cycles * tau);

			movementFactor = 0.5f + rawSineWave / 2f;
		} else{
			movementFactor = 0;
		}
		
		Vector3 offset = movementVector * movementFactor;
		transform.position = startPosition + offset;
	}
}
