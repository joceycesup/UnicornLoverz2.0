using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULCharacterController : MonoBehaviour {
	public float speed = 4.0f;

	void Start () {

	}

	void FixedUpdate () {
		Vector3 direction = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")).normalized;
		transform.Translate (direction * speed * Time.fixedDeltaTime);
	}
}
