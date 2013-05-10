using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {
	
	public Vector3 offset, rotationVelocity;
	public float recycleOffset, spawnChance;
	
	// Use this for initialization
	void Start () {
		GameEventManager.GameOver += GameOver;
		GameEventManager.GameStart += GameStart;
		gameObject.active = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.x + recycleOffset < ConstantCharacterMovement.distanceTraveled){
			gameObject.active = false;
			return;
		}
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}
	
	void OnTriggerEnter () {
		ConstantCharacterMovement.AddBoost();
		gameObject.active = false;
	}

	public void SpawnIfAvailable (Vector3 position) {
		if(gameObject.active || spawnChance <= Random.Range(0f, 100f)) {
			return;
		}
		transform.localPosition = position + offset;
		gameObject.active = true;
	}

	private void GameOver () {
		gameObject.active = false;
	}
	
	private void GameStart () {
		gameObject.active = false;
	}
}