using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    private Vector3 offSet;
	// Use this for initialization
	void Start () {
        offSet = transform.position - target.position;
	}

    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = target.position + offSet;
    }
}
