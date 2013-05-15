using UnityEngine;
using System.Collections;

public class ConstantCharacterMovement : MonoBehaviour {
	
	// These are available in the inspector.
	public float movementSpeed = 1.0f; // Movement speed of the character.
	public float acceleration;
	public Vector3 boostVelocity, jumpVelocity;
	public float gameOverY;

	private bool touchingPlatform;
	public static float distanceTraveled;
	private Vector3 startPosition;
	private static int boosts;
	
	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		gameObject.active = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")){
			if(touchingPlatform){
				rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
				touchingPlatform = false;
			}
			else if(boosts > 0){
				rigidbody.AddForce(boostVelocity, ForceMode.VelocityChange);
				boosts -= 1;
				GUIManager.SetBoosts(boosts);
			}
		}
		distanceTraveled = transform.localPosition.x;
		GUIManager.SetDistance(distanceTraveled);
		
		if(transform.localPosition.y < gameOverY){
			GameEventManager.TriggerGameOver();
		}
		
		// Move camera according to velocity
		//Camera camera = gameObject.GetComponentInChildren<Camera>();
		//camera.orthographicSize = gameObject.rigidbody.velocity.y;
	}
	
	void FixedUpdate () {
		if(touchingPlatform){
			rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}

	void OnCollisionEnter () {
		touchingPlatform = true;
	}

	void OnCollisionExit () {
		touchingPlatform = false;
	}
	
	private void GameStart () {
		boosts = 0;
		GUIManager.SetBoosts(boosts);
		distanceTraveled = 0f;
		GUIManager.SetDistance(distanceTraveled);
		transform.localPosition = startPosition;
		rigidbody.isKinematic = false;
		rigidbody.velocity = Vector3.zero;
		gameObject.active = true;
		enabled = true;
	}
	
	private void GameOver () {
		rigidbody.isKinematic = true;
		enabled = false;
	}
	
	public static void AddBoost(){
		boosts += 1;
		GUIManager.SetBoosts(boosts);
	}
}
