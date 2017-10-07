using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ULCharacter : MonoBehaviour {
	public bool toTheRight = true;
	protected SpriteRenderer sr;
	protected Animator animator;

	protected void Init () {
		sr = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
	}

	void FixedUpdate () {
		Vector3 axis = GetAxis ();
		if (axis.x != 0.0f) {
			if (toTheRight != (toTheRight = axis.x > 0.0f)) {
				sr.flipX = !toTheRight;
			}
		}
		Vector3 direction = new Vector3 (axis.x, axis.y).normalized;
		if (direction.x != 0.0f && direction.y != 0.0f) {
			animator.Play ("Walk");
		}
		else {
			animator.Play ("Idle");
		}
		transform.Translate (direction * speed * Time.fixedDeltaTime);
	}

	protected abstract Vector3 GetAxis ();
}
