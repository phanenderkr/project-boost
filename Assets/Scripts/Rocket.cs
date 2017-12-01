using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour{
	private Rigidbody rigidBody;
	private AudioSource audioSource;
	
	[SerializeField]
	float rcsThrust = 100f, mainThrust =1000f;

	[SerializeField] private AudioClip mainEngineClip, deathClip, successClip; 
	
	[SerializeField] private ParticleSystem  mainEngineParticleSystem,deathParticleSystem, 
	successParticleSystem;

	[SerializeField] private bool collisionsAvailable = true;

	[SerializeField] private float levelLoadDelay = 1f;
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
		if (state == State.Alive){
			RespondToThrustInput();
			RespondToRotateInput();
		}
		if (Debug.isDebugBuild){
			DebugKeys();
		}
		
	}
	
	//---------------------------------------------------------------------------------
	//-----------------------------------Debug Keys------------------------------------
	//---------------------------------------------------------------------------------
	void DebugKeys(){
		if (Input.GetKeyDown(KeyCode.L)){
			LoadNextLevel();
		}else if (Input.GetKeyDown(KeyCode.C)){
			if (collisionsAvailable){
				collisionsAvailable = false;
			} else{
				collisionsAvailable = true;
			}
		}
	}
	
	
	//---------------------------------------------------------------------------------
	//--------------------------------Motion------------------------------------------
	//---------------------------------------------------------------------------------
	void RespondToRotateInput(){
		rigidBody.angularVelocity = Vector3.zero;
		float rotationSpeed = rcsThrust * Time.deltaTime;
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			transform.Rotate(Vector3.forward * rotationSpeed);
		} else if (Input.GetKey(KeyCode.D)  || Input.GetKey(KeyCode.RightArrow)){
			transform.Rotate(Vector3.back * rotationSpeed);
		}
	}

	private void RespondToThrustInput(){
		if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) ){
			ApplyThrust();
		}
		if (Input.GetKeyUp(KeyCode.Space)|| Input.GetKeyUp(KeyCode.UpArrow)){
			audioSource.Stop();
			mainEngineParticleSystem.Stop();
		}
	}

	private void ApplyThrust(){
		float thrustSpeed = mainThrust * Time.deltaTime;
		rigidBody.AddRelativeForce(Vector3.up * thrustSpeed);
		if (!audioSource.isPlaying){
			audioSource.PlayOneShot(mainEngineClip);
		}
		mainEngineParticleSystem.Play();
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
		mainEngineParticleSystem.Stop();
		successParticleSystem.Play();
		state = State.Transcending;
		Invoke("LoadNextLevel", levelLoadDelay);
	}

	private void StartDeathSequence(){
		if (collisionsAvailable){
			audioSource.Stop();
			audioSource.PlayOneShot(deathClip);
			mainEngineParticleSystem.Stop();
			deathParticleSystem.Play();
			state = State.Dying;
			Invoke("LoadFirstLevel", levelLoadDelay);
		}
	}
	
	private void LoadFirstLevel(){
		SceneManager.LoadScene(0);
	}

	private void LoadNextLevel(){
		Scene scene = SceneManager.GetActiveScene();
		int buildIndex = scene.buildIndex;
		print(SceneManager.sceneCountInBuildSettings);
		if (buildIndex < SceneManager.sceneCountInBuildSettings-1){
			SceneManager.LoadScene(buildIndex + 1);
		} else{
			SceneManager.LoadScene(0);
		} 
	}
}
