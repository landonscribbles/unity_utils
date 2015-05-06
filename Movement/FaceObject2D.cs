using UnityEngine;
using System.Collections;

public class FaceObject2D : MonoBehaviour {
	public float faceSpeed = 100.0f;
	public GameObject targetObject;
	public bool faceOnce = true;

	private bool faced = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!faced){
			Quaternion newRotation = Quaternion.LookRotation (targetObject.transform.position - transform.position, -Vector3.forward);
			newRotation.x = 0;
			newRotation.y = 0;
			
			if (transform.position != targetObject.transform.position) {
				transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * faceSpeed);
			}
			if(faceOnce){
				faced = true;
			}
		}
	}

	public static Quaternion getDirectionToObject(GameObject source, GameObject target){
		Quaternion newRotation = Quaternion.LookRotation (target.transform.position - source.transform.position, -Vector3.forward);
		newRotation.x = 0;
		newRotation.y = 0;
		
		if (source.transform.position != target.transform.position) {
			source.transform.rotation = Quaternion.Slerp (source.transform.rotation, newRotation, Time.deltaTime * 100);
		}
		return newRotation;
	}
}
