using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {

	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 minimumSize, maximumSize, minimumGap, maximumGap;
	public float minimumY, maximumY;
	public Material[] materials;
	public PhysicMaterial[] physicMaterials;
	public Booster booster;
	
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
		booster.SpawnIfAvailable(position);
		
		Transform objectTransform = objectQueue.Dequeue();
		objectTransform.localScale = scale;
		objectTransform.localPosition = position;
		nextPosition.x += scale.x;
		int materialIndex = Random.Range(0, materials.Length);
		objectTransform.renderer.material = materials[materialIndex];
		objectTransform.collider.material = physicMaterials[materialIndex];
		objectQueue.Enqueue(objectTransform);
		
		nextPosition += new Vector3(
			Random.Range(minimumGap.x, maximumGap.x) + scale.x,
			Random.Range(minimumGap.y, maximumGap.y),
			Random.Range(minimumGap.z, maximumGap.z));
		
		if (nextPosition.y < minimumY){
			nextPosition.y = minimumY + maximumGap.y;
		}
		else if (nextPosition.y > maximumY){
			nextPosition.y = maximumY - maximumGap.y;
		}
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
