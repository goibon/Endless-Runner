using UnityEngine;
using System.Collections;

public class MyController : MonoBehaviour {
	public int speed = 7;
	public bool canMoveLeft = false;
	public bool canMoveRight = true;
	public bool useGravity = true;
	public float gravity = 5.0F;
	public float jumpTime = 1.0F;
	public float jumpHeight = 1.0F;
	
	private CharacterController controller;
	private Vector3 move = new Vector3(0,0,0);
	private Vector3 jump = new Vector3(0,0,0);
	private float timeSinceGrounded;
	private float previousHeight;
	private float maxHeight;
		
	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent("CharacterController") as CharacterController;
	}
	
	// Update is called once per frame
	void Update () {
		MoveLeftRight();
		Jump();
		Gravity();
	}
	
	// This moves the gameobject horizontally
	void MoveLeftRight () {
		if (Input.GetAxisRaw("Horizontal") > 0 && canMoveRight)
		{ 
			move.x = speed * Time.deltaTime * 2;
			controller.Move(move);
		}
		else if (Input.GetAxisRaw("Horizontal") < 0 && canMoveLeft)
		{
			move.x = -(speed * Time.deltaTime * 2);
			controller.Move(move);
		}
		move.x = 0;
	}
	
	// This moves the gameobject up
	void Jump(){
		
		// Resets the properties if the gameobject is grounded
		if (controller.isGrounded)
		{
			jumpTime = 0.2F;
			previousHeight = controller.transform.position.y;
			maxHeight = previousHeight + jumpHeight;
		}
		
		// Makes the gameobject jump if jumpkey is pressed and it hasn't reached the peak height
		if (Input.GetButton("Jump") && controller.transform.position.y < maxHeight)
		{
			jumpTime -= Time.deltaTime;
			jump.y = speed * Time.deltaTime * 3;
			controller.Move(jump);
		}
		
		// Hinders the user from jumping midair
		if (!Input.GetButton("Jump") || controller.transform.position.y > (maxHeight-1))
			maxHeight = 0;
		
//		if (Input.GetButton("Jump") && jumpTime > 0)
//		{
//			jumpTime -= Time.deltaTime;
//			jump.y = speed * Time.deltaTime * 3;
//			controller.Move(jump);
//		}
//		
//		if (!Input.GetButton("Jump") && jumpTime >0)
//			jumpTime = 0;

	}
	
	// This moves the gameobject down
	void Gravity(){
		if (!controller.isGrounded && useGravity)
			{
				jump.y -= gravity * Time.deltaTime;
				controller.Move(jump);
			}
	}
}