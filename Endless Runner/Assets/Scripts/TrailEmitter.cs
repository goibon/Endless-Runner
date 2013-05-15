using UnityEngine;
using System.Collections;

public class TrailEmitter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameEventManager.GameOver += GameOver;
		GameEventManager.GameStart += GameStart;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void GameOver () {
		particleSystem.Pause();
	}
	
	private void GameStart () {
		particleSystem.Clear();
		particleSystem.Play();
	}
}
