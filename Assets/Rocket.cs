using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour{
	private Rigidbody rigidBody;
	private AudioSource audioSource;
	
	[SerializeField]
	float rcsThrust = 100f, mainThrust =1000f;

	[SerializeField] private AudioClip mainEngine, deathClip, successClip; 

	enum State{
		Alive,
		Dying, 
		Transcending
	}

	private State state = State.Alive;
	
	// Use this for initialization
	void Start (){
		rigidBody = gameObject.GetComponent<Rigidbody>();
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update (){
		//todo somewhere stop the sound
		if (state == State.Alive){
			RespondToThrustInput();
			RespondToRotateInput();
		}
	}

	//---------------------------------------------------------------------------------
	//--------------------------------Motion------------------------------------------
	//---------------------------------------------------------------------------------
	void RespondToRotateInput(){
		rigidBody.freezeRotation = true;
		float rotationSpeed = rcsThrust * Time.deltaTime;
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			transform.Rotate(Vector3.forward * rotationSpeed);
		} else if (Input.GetKey(KeyCode.D)  || Input.GetKey(KeyCode.RightArrow)){
			transform.Rotate(Vector3.back * rotationSpeed);
		}
		rigidBody.freezeRotation = false;
	}

	private void RespondToThrustInput(){
		if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) ){
			ApplyThrust();
		}
		if (Input.GetKeyUp(KeyCode.Space)|| Input.GetKeyUp(KeyCode.UpArrow)){
			audioSource.Stop();
		}
	}

	private void ApplyThrust(){
		float thrustSpeed = mainThrust * Time.deltaTime;
		rigidBody.AddRelativeForce(Vector3.up * thrustSpeed);
		if (!audioSource.isPlaying){
			audioSource.PlayOneShot(mainEngine);
		}
	}

	//---------------------------------------------------------------------------------
	//--------------------------------Collision----------------------------------------
	//---------------------------------------------------------------------------------
	
	private void OnCollisionEnter(Collision other){
		
		// prevents the execution the future statements
		if (state != State.Alive){
			return;
		}
		
		switch (other.gameObject.tag){
			case "Friendly":
				print("Ok");
				break;
			case "Finish":
				StartFinishSquence();
				break;
			default:
				StartDeathSequence();
				break;
		}
	}

	private void StartFinishSquence(){
		audioSource.Stop();
		audioSource.PlayOneShot(successClip);
		state = State.Transcending;
		Invoke("LoadNextLevel",1f);
	}

	private void StartDeathSequence(){
		audioSource.Stop();
		audioSource.PlayOneShot(deathClip);
		state = State.Dying;
		Invoke("LoadFirstLevel",1f);
	}
	
	private void LoadFirstLevel(){
		SceneManager.LoadScene(0);
	}

	private void LoadNextLevel(){
		SceneManager.LoadScene(1); // todo allow more than 2 levels
	}
}
