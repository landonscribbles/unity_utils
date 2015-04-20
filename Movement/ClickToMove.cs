/*
 * Info: 
 * Moves object and faces object to a position you click. Requires a gameobject to raycast against.
 * May prohibit movement below an interactive UI element with GUIUtility.hotControl but untested.
 * 
 * Tested on: 
 * Winders
 * 
 * Known Issues:
 * None.
 */
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour {
    private Vector3 position;

    public float speed = 1.0f;

    void Start() {

    }

    void Update() {
        if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0) {
            locatePosition();
        }

        if (position != null) {
            moveToPosition();
        }
    }

    void locatePosition() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100)) {
            position = new Vector3(hit.point.x, hit.point.y);
        }
    }

    void moveToPosition() {
        Quaternion newRotation = Quaternion.LookRotation(position - transform.position, Vector3.forward);
        newRotation.x = 0;
        newRotation.y = 0;

        if (transform.position != position) {
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
        }

        transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * speed);
    }
}
