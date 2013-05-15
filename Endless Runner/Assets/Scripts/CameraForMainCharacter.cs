using UnityEngine;
using System.Collections;

public class CameraForMainCharacter : MonoBehaviour {
	
	public float minimumSize, maximumSize; // The closest / farthest away the camera will be to / from the scene it's pointing towards.
	public float minimumVelocity, maximumVelocity; // The velocities that camera size depends on.
	public GameObject target;
	
	private float targetVelocityX, sizeRange, velocityRange, percent, newSize;
	
	// Use this for initialization
	void Start () {
		GameEventManager.GameOver += GameOver;
		GameEventManager.GameStart += GameStart;
		sizeRange = maximumSize - minimumSize;
		if (sizeRange == 0){
			Debug.LogError("sizeRange is 0.");
		}
		velocityRange = maximumVelocity - minimumVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		targetVelocityX = target.rigidbody.velocity.x;
		if (targetVelocityX < minimumVelocity)
		{
			camera.orthographicSize = minimumSize;
		}
		else if (targetVelocityX < maximumVelocity && sizeRange != 0){
			percent = (targetVelocityX - minimumVelocity) / (velocityRange);
			newSize = (sizeRange) * percent + minimumSize;
			camera.orthographicSize = newSize;
		}
		else
		{
			camera.orthographicSize = maximumSize;
		}
	}
	
	private void GameOver(){
		enabled = false;
	}
	
	private void GameStart(){
		enabled = true;
	}
}
