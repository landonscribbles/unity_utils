/* Info: Have the main camera follow the player on the X and Y axis.
 * Was tested with an Orthographic camera
 * 
 * Tested on:
 * Winders
 * 
 * Known Issues:
 * None (yet)
 * 
*/
using UnityEngine;
using System.Collections;

public class CenterCameraOnObject : MonoBehaviour {
    public GameObject followedObject;

	void Update () {
        Vector3 curretCameraLoc = Camera.main.transform.position;
        Vector3 newCameraLoc = new Vector3(
            followedObject.transform.position.x, followedObject.transform.position.y, curretCameraLoc.z
        );
        Camera.main.transform.position = newCameraLoc;
	}
}
