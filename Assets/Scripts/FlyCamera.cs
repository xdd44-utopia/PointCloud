using UnityEngine;
using System.Collections;
 
public class FlyCamera : MonoBehaviour {
 
	/*
	Writen by Windexglow 11-13-10.  Use it, edit it, steal it I don't care.  
	Converted to C# 27-02-13 - no credit wanted.
	Simple flycam I made, since I couldn't find any others made public.  
	Made simple to use (drag and drop, done) for regular keyboard layout  
	wasd : basic movement
	shift : Makes camera accelerate
	space : Moves camera on X and Z axis only.  So camera doesn't gain any height*/
	 
	 
	float mainSpeed = 10.0f; //regular speed
	float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
	float maxShift = 1000.0f; //Maximum speed when holdin gshift
	float camSens = 1f; //How sensitive it with mouse
	private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
	private float totalRun= 1.0f;
	 
	void Update () {
		lastMouse = Input.mousePosition - lastMouse ;
		lastMouse = new Vector3(-Input.GetAxis("Mouse Y") * camSens, Input.GetAxis("Mouse X") * camSens, 0 );
		lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
		lastMouse.x = (lastMouse.x <= 90 || lastMouse.x >= 270 ? lastMouse.x : (lastMouse.x <= 180 ? 90 : 270));
		transform.eulerAngles = lastMouse;
		lastMouse =  Input.mousePosition;
		//Mouse  camera angle done.  
	   
		//Keyboard commands
		float f = 0.0f;
		Vector3 p = GetBaseInput();
		if (p.sqrMagnitude > 0){ // only move while a direction key is pressed
		  if (Input.GetKey (KeyCode.LeftShift)){
			  totalRun += Time.deltaTime;
			  p  = p * totalRun * shiftAdd;
			  p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
			  p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
			  p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
		  } else {
			  totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
			  p = p * mainSpeed;
		  }
		 
		  p = p * Time.deltaTime;
		  Vector3 newPosition = transform.position;
		  if (Input.GetKey(KeyCode.Space)){ //If player wants to move on X and Z axis only
			  transform.Translate(p);
			  newPosition.x = transform.position.x;
			  newPosition.z = transform.position.z;
			  transform.position = newPosition;
		  } else {
			  transform.Translate(p);
		  }
		}
		
		if (Input.GetKey(KeyCode.Space)) {
			Cursor.lockState = CursorLockMode.Locked;
		}
		if (Input.GetKey(KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
		}

	}
	 
	private Vector3 GetBaseInput() { //returns the basic values, if it's 0 than it's not active.
		Vector3 p_Velocity = new Vector3();
		if (Input.GetKey (KeyCode.W)){
			p_Velocity += new Vector3(0, 0 , 1);
		}
		if (Input.GetKey (KeyCode.S)){
			p_Velocity += new Vector3(0, 0, -1);
		}
		if (Input.GetKey (KeyCode.A)){
			p_Velocity += new Vector3(-1, 0, 0);
		}
		if (Input.GetKey (KeyCode.D)){
			p_Velocity += new Vector3(1, 0, 0);
		}
		if (Input.GetKey (KeyCode.Q)){
			p_Velocity += new Vector3(0, -1, 0);
		}
		if (Input.GetKey (KeyCode.E)){
			p_Velocity += new Vector3(0, 1, 0);
		}
		return p_Velocity;
	}
}