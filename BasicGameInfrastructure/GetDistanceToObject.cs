using UnityEngine;
using System.Collections;

public class GetDistanceToObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static float getDistanceToObject(GameObject sourceObj, GameObject targetObject){
		return Vector3.Distance(sourceObj.transform.position, targetObject.transform.position);
	}

	public static float getDistanceToObject(Vector3 sourcePosition, Vector3 targetPosition){
		return Vector3.Distance (sourcePosition, targetPosition);
	}
}
