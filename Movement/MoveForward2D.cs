/*
 * Info: 
 * Move forward (up) in 2D (from the top of an image)!
 * 
 * Tested on: 
 * Android device and DT computron.  Only tested with Orthographic camera (2D).
 * 
 * Known Issues:
 * I'm fairly sure this will not work on 3D camera.
 */
using UnityEngine;
using System.Collections;

public class MoveForward2D : MonoBehaviour {
	public float speed = 10.0f;

	private bool move = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (move) {
			transform.position += transform.up * Time.deltaTime * speed;
		}
	}

	public void setMove(bool move){
		this.move = move;
	}

	public bool getMove(){
		return move;
	}
}
