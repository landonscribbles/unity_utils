/*
 * Info: 
 * Adds devices to a Dictionary as they enter into a 2d Collider and removes them when they exit.
 * Has helper methods to get the closest object etc.
 * 
 * Tested on: 
 * Android device.  Only tested with one object so far.  Could blow up with multiple objects for all I know.
 * 
 * Known Issues:
 * May have issues with object spawning into it.  Not sure if that's picked up by "OnTriggerEnter2D"
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetCloseObject : MonoBehaviour {
	public string objectTag = "targetable";
	public GameObject centerObject;

	private GameObject closeObject;

	private Dictionary<int, GameObject> closeObjects = new Dictionary<int, GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log ("on trigger enter");
		if (collider.gameObject.tag == objectTag) {
			GameObject targetedObject = collider.gameObject;
			closeObjects.Add(targetedObject.GetInstanceID(), targetedObject);
			Debug.Log("Added object");
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		Debug.Log ("On triggere exit");
		if (collider.gameObject.tag == objectTag) {
			closeObjects.Remove(collider.gameObject.GetInstanceID());
			Debug.Log("Removed Object");
		}
	}

	public GameObject getCloseObject(){
		float distance = -1;
		float currentObjectDistance;
		GameObject closestObject = null;

		foreach (KeyValuePair<int, GameObject> entry in closeObjects) {
			if(distance == -1){
				closestObject = entry.Value;
				distance = Vector3.Distance(closestObject.transform.position, centerObject.transform.position);  
			}else{
				GameObject currentObject = entry.Value;
				currentObjectDistance = Vector3.Distance(currentObject.transform.position, centerObject.transform.position);

				if(currentObjectDistance < distance){
					distance = currentObjectDistance;
					closestObject = currentObject;
				}
			}
		}

		return closestObject;
	}

	public int getNumberOfObjects(){
		return closeObjects.Count;
	}
}
