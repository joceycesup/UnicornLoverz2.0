using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULCharacterController : MonoBehaviour {
	public float speed = 4.0f;
	public float hugDistance = 1.0f;

	void Start () {

	}

	private void Update () {
		if (Input.GetButtonDown("Hug")) {
			Physics2D.Raycast (transform.position, Vector2.right, hugDistance);
		}
	}

	void FixedUpdate () {
		Vector3 direction = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")).normalized;
		transform.Translate (direction * speed * Time.fixedDeltaTime);
	}

	public void Push (Vector3 force) {
		transform.Translate (force);
	}
}
