using UnityEngine;
using System.Collections;

public class My2DController : MonoBehaviour {
	
	// Public Variables
	public float speed = 0.5F;
	
	// Private Variables
	private CharacterController controller;
	private Vector2 move = new Vector2(0,0);
	
	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent("CharacterController") as CharacterController;
	}
	
	// Update is called once per frame
	void Update () {
		MoveLeftRight();
	}
	
	/// <summary>
	/// Moves the controller left or right.
	/// </summary>
	void MoveLeftRight()
	{
		if (Input.GetAxisRaw("Horizontal") > 0)
		{
		move.x = speed;
		controller.Move(move);
		}
		
		if (Input.GetAxisRaw("Horizontal") < 0)
		{
			move.x = -speed;
			controller.Move(move);
		}
	}
}
