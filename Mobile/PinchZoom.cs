/*
 * Info: 
 * Pinch zoom functionality for touch devices.
 * 
 * Tested on: 
 * Android device.  Only tested with Orthographic camera (2D).
 * 
 * Known Issues:
 * Needs to be tested on 3D camera and probably tweaked to have it working properly.
 */
using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour {
	public Camera selectedCamera;

	public float zoomSpeed = 1.0f;

	public float MAXZOOM = 20;
	public float MINZOOM = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 2) {
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
			// If the camera is orthographic...
			if (selectedCamera.isOrthoGraphic)
			{
				// ... change the orthographic size based on the change in distance between the touches.
				selectedCamera.orthographicSize += deltaMagnitudeDiff * zoomSpeed;
				if(selectedCamera.orthographicSize > MAXZOOM){
					selectedCamera.orthographicSize = MAXZOOM;
				}
				else if(selectedCamera.orthographicSize < MINZOOM){
					selectedCamera.orthographicSize = MINZOOM;
				}
				// Make sure the orthographic size never drops below zero.
				selectedCamera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
			}
			else
			{
				// Otherwise change the field of view based on the change in distance between the touches.
				camera.fieldOfView += deltaMagnitudeDiff * zoomSpeed;
				
				// Clamp the field of view to make sure it's between 0 and 180.
				camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
			}
		}
	}
}
