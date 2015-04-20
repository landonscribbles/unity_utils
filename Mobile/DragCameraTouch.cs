/*
 * Info:
 * Scroll camera in the direction your finger slides.
 * Set up to use 3 finger touch but you can change that (numberOfTouchesRequired = 3).
 * 
 * Tested on: 
 * Android device.  Only tested with Orthographic camera (2D).
 * 
 * Known Issues:
 * Needs to be tested on 3D camera and probably tweaked to have it working properly.
 */
using UnityEngine;
using System.Collections;

public class DragCameraTouch : MonoBehaviour {
	private int numberOfTouchesRequired = 3;
	public Camera selectedCamera;

	public float cameraMoveSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount == numberOfTouchesRequired && Input.GetTouch(0).phase == TouchPhase.Moved){
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			selectedCamera.transform.Translate(-touchDeltaPosition.x * cameraMoveSpeed, -touchDeltaPosition.y * cameraMoveSpeed, 0);
		}
	}
}
