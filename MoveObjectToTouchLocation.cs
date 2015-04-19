/*
 * Info: 
 * Moves object and faces object to a position you touch.
 * Will not move to location below a UI element (UI elements block the touch).
 * 
 * Tested on: 
 * Android tablet.
 * 
 * Known Issues:
 * None.
 */
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MoveObjectToTouchLocation : MonoBehaviour {
	private Vector3 position;

	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0) && Input.touchCount == 1) {
			//Locate the location clicked
			if(!EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId)){
				locatePosition();
			}
		}

		if(position != null){
			moveToPosition ();
		}
	}

	void locatePosition(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast (ray, out hit, 100)) {
			position = new Vector3(hit.point.x, hit.point.y);	
			Debug.Log(position);
		}
	}

	void moveToPosition(){
		Quaternion newRotation = Quaternion.LookRotation (position - transform.position, Vector3.forward);
		newRotation.x = 0;
		newRotation.y = 0;
		
		if (transform.position != position) {
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 10);
		}

		transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * speed);
	}
}
