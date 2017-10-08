using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULMilitia : ULCharacter {
	private bool hunter = true;
	private bool idle = false;
	private ULFollowerController militiaTarget = null;
	private Vector3 direction;

	protected override void Init () {
		base.Init ();
		enabled = false;
	}

	private void OnEnable () {
		Debug.Log (gameObject);
	}

	protected override Vector3 GetAxis () {
		return direction;
	}

	public override void RunAway () {
		militiaTarget.tag = "Untagged";
		militiaTarget = null;
		hunter = false;
		base.RunAway ();
	}

	public override void Hugged (ULPlayerController player) {
		player.Push (Vector3.Normalize (player.transform.position - transform.position) * ULGlobals.militiaPushForce);
	}

	protected override void CharFixedUpdate () {
		if (idle)
			return;
		Transform target = (militiaTarget != null) ? militiaTarget.transform : ULGlobals.player.transform;

		direction = target.position - transform.position;
		float distance = direction.magnitude;
		direction = Vector3.Normalize (direction) * ULGlobals.militiaSpeed * Time.fixedDeltaTime;
		if (militiaTarget != null) {
			Debug.DrawLine (transform.position, militiaTarget.transform.position, Color.red);
		}
		else if ((this.transform.position - target.position).magnitude <= ULGlobals.militiaChooseRadius) {
			ChooseTarget ();
		}

		if (direction.magnitude > distance)
			direction = Vector3.ClampMagnitude (direction, distance);
		transform.Translate (direction);

		base.CharFixedUpdate ();
	}

	public void ChooseTarget () {
		Transform overlapOrigin = ULGlobals.player.transform;
		Collider2D[] results = Physics2D.OverlapCircleAll (overlapOrigin.position, ULGlobals.militiaChooseRadius, 256); // Huggable (1 << 8)
		foreach (Collider2D c in results) {
			if (TakeAsTarget (c.GetComponent<ULFollowerController> ()))
				break;
		}
		if (militiaTarget == null)
			foreach (ULFollowerController f in FindObjectsOfType<ULFollowerController> ()) {
				if (TakeAsTarget (f))
					break;
			}
		if (militiaTarget == null)
			ULGameStateHandler.Reload ();
	}

	private bool TakeAsTarget (ULFollowerController follower) {
		if (follower == null)
			return false;
		if (follower.state != ULFollowerController.FollowerState.Gai)
			return false;
		if (follower.CompareTag ("MilitiaTarget"))
			return false;
		follower.tag = "MilitiaTarget";
		militiaTarget = follower;
		Debug.Log ("Took " + militiaTarget + " as target");
		return true;
	}

	private void OnCollisionEnter2D (Collision2D collision) {
		if (militiaTarget == null)
			return;
		if (collision.gameObject.Equals (militiaTarget.gameObject)) {
            animator.Play("Handcuffed");
			militiaTarget.Handcuff (this);
			idle = true;
		}
	}
}
