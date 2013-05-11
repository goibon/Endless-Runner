using UnityEngine;
using System.Collections;

public class WeatherManager : MonoBehaviour {
	
	public Vector3 offset;
	public Transform target;
	public ParticleSystem[] weatherEmitters;
	
	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		if (target == null)
		{
			Debug.LogError("Missing target.");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void GameStart () {
		int weatherIndex = Random.Range(0, weatherEmitters.Length);
		GameObject[] weathers = GameObject.FindGameObjectsWithTag("Weather");
		if (weathers.Length > 0)
		{
			foreach (GameObject weather in weathers)
			{
				//weather.transform.particleSystem.enableEmission = true;
				Destroy(weather);
			}
			ParticleSystem weatherInstance = (ParticleSystem)Instantiate(weatherEmitters[weatherIndex], offset, Quaternion.identity);
			weatherInstance.transform.parent = target;
		}
		else // For first time initialization
		{
			ParticleSystem weatherInstance = (ParticleSystem)Instantiate(weatherEmitters[weatherIndex], offset, Quaternion.identity);
			weatherInstance.transform.parent = target;
		}
	}
	
	void GameOver (){
		if (GameObject.FindGameObjectsWithTag("Weather").Length > 0){
			foreach (GameObject weather in GameObject.FindGameObjectsWithTag("Weather"))
			{
				weather.transform.particleSystem.Pause();
			}
		}
	}
}
