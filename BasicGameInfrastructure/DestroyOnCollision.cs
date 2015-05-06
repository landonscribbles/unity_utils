/*
 * Info: 
 * Destroy object on collision with specified tag.
 * 
 * Tested on: 
 * Android device + computron
 * 
 * Known Issues:
 * Will only work on 2D.
 * 
 */
using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour {
	public string collideWithTag = "targetable";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == collideWithTag) {
			Destroy (gameObject);
		}
	}
}
