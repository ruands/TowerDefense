using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField] float moveSpeed = 10f;
	[SerializeField] float mouseScrollSpeed = 50f;
	
	void Update () {
        float forwardBackward = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		float verticalMovement = Input.GetAxis("Mouse ScrollWheel") * mouseScrollSpeed * Time.deltaTime;

        Vector3 newPosition = new Vector3(horizontalMovement, -verticalMovement, forwardBackward);
        transform.Translate(newPosition, Space.World);
	}
}
