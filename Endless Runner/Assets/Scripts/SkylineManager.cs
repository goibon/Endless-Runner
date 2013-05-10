using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkylineManager : MonoBehaviour {
	
	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 minimumSize, maximumSize;
	
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;

	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		objectQueue = new Queue<Transform>(numberOfObjects);
		for(int i = 0; i < numberOfObjects; i++){
			objectQueue.Enqueue((Transform)Instantiate
			                    (prefab, new Vector3(0f, 0f, -100f), Quaternion.identity));
		}
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(objectQueue.Peek().localPosition.x + recycleOffset < ConstantCharacterMovement.distanceTraveled){
			Recycle();
		}
	}
	
	private void Recycle () {
		Vector3 scale = new Vector3(
			Random.Range(minimumSize.x, maximumSize.x),
			Random.Range(minimumSize.y, maximumSize.y),
			Random.Range(minimumSize.z, maximumSize.z));

		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;
		
		Transform objectTransform = objectQueue.Dequeue();
		objectTransform.localScale = scale;
		objectTransform.localPosition = position;
		nextPosition.x += scale.x;
		objectQueue.Enqueue(objectTransform);
	}
	
	private void GameStart () {
		nextPosition = transform.localPosition;
		for(int i = 0; i < numberOfObjects; i++){
			Recycle();
		}
		enabled = true;
	}

	private void GameOver () {
		enabled = false;
	}
}
