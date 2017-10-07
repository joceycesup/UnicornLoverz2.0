using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULCharacterController : ULCharacter {
	public float speed = 4.0f;
	public float hugDistance = 1.0f;

	void Start () {
		base.Init ();
	}

	private void Update () {
		if (Input.GetButtonDown ("Hug")) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, toTheRight ? Vector2.right : Vector2.left, hugDistance, 8); // Huggable
			Debug.DrawRay (transform.position, (toTheRight ? Vector2.right : Vector2.left) * hugDistance, Color.red, 0.5f);
			if (hit.collider != null) {
				Debug.Log ("Hugged " + hit.transform);
			}
			else {
				Debug.Log ("Didn't hug");
			}
		}
	}

	public void Push (Vector3 force) {
		transform.Translate (force);
	}

	protected override Vector3 GetAxis () {
		return new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
	}
}
