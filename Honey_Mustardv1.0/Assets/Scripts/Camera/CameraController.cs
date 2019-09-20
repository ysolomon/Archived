using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	int cameraVelocity = 10;

	public float min_x, min_y, max_x, max_y;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		// Left
		if((Input.GetKey(KeyCode.LeftArrow)) && transform.position.x >= min_x) {
			transform.Translate((Vector3.left* cameraVelocity) * Time.deltaTime);
		}
		// Right
		if((Input.GetKey(KeyCode.RightArrow)) && transform.position.x <= max_x) {
			transform.Translate((Vector3.right * cameraVelocity) * Time.deltaTime);
		}
		// Up
		if((Input.GetKey(KeyCode.UpArrow)) && transform.position.y <= max_y) {
			transform.Translate((Vector3.up * cameraVelocity) * Time.deltaTime);
		}
		// Down
		if(Input.GetKey(KeyCode.DownArrow) && transform.position.y >= min_y) {
			transform.Translate((Vector3.down * cameraVelocity) * Time.deltaTime);
		}
	}
}
