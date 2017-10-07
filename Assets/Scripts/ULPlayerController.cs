using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULPlayerController : ULCharacter {
	public float hugDistance = 1.0f;
	private SpriteRenderer halo;

	protected override void Init () {
		base.Init ();
		halo = transform.GetChild (0).GetComponent<SpriteRenderer> ();
	}

	protected override void CharUpdate () {
		base.CharUpdate ();
		if (Input.GetButtonDown ("Hug")) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, toTheRight ? Vector2.right : Vector2.left, hugDistance, 1 << 8); // Huggable
			Debug.DrawRay (transform.position, (toTheRight ? Vector2.right : Vector2.left) * hugDistance, Color.magenta, 0.5f);
			if (hit.collider != null) {
				hit.collider.GetComponent<ULCharacter> ().Hugged (this);
			}
			else {
				Debug.Log ("Didn't hug");
			}
		}
		transform.Translate (GetAxis () * ULGlobals.playerSpeed * Time.fixedDeltaTime);

		halo.color = new Color (1.0f, 1.0f, 1.0f, Mathf.Clamp01 (ULFollowerController.gaiCount / ULGlobals.maxFollowers));
	}

	public void Push (Vector3 force) {
		transform.Translate (force);
	}

	protected override Vector3 GetAxis () {
		return new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")).normalized;
	}
}
