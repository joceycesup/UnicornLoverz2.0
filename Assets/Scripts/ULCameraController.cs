using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULCameraController : MonoBehaviour {
	public float minSize = 1.0f, maxSize = 5.0f;
	public float maxCrowd = 100;
	private ULCharacterController target;
	[Range (0.0f, 130.0f)]
	public float crowd = 1.0f;

	// Use this for initialization
	void Start () {
		target = FindObjectOfType<ULCharacterController> ();
	}

	void Update () {
		transform.position = new Vector3 (target.transform.position.x, 0.0f, transform.position.z);
		Camera.main.orthographicSize = Mathf.Lerp (minSize, maxSize, crowd/ maxCrowd );
	}
}
