using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULPlayerController : ULCharacter {
	public Vector2 hugHalfBox = new Vector2 (8.0f, 14.0f);
	public float hugDistance = 8.0f;
	public SpriteRenderer halo;

	private bool isHugging = false;

	protected override void Init () {
		base.Init ();
		halo = transform.GetChild (0).GetComponent<SpriteRenderer> ();
	}

	protected override void CharUpdate () {
		if (isHugging)
			return;
		base.CharUpdate ();
		if (Input.GetButtonDown ("Hug")) {
			Collider2D coll = GetTarget ();
			isHugging = true;
			if (coll != null) {
				if (coll.GetComponent<ULCharacter> ().Hugged (this)) {
					ULFollowerController fol = coll.GetComponent<ULFollowerController> ();
					if (fol.state == ULFollowerController.FollowerState.Boss) {
						animator.Play ("HugBoss");
						Destroy (this);
					}
					StartCoroutine (HugAnim (fol));
				}
				else
					isHugging = false;
				//animator.Play("Hug");
			}
			else {
				animator.Play ("EmptyHug");
			}
			StartCoroutine (HugFollower (coll == null));
		}
		else if (Input.GetButtonDown ("Hit")) {
			Collider2D coll = GetTarget ();
			if (coll != null) {
				isHugging = true;
				ULFollowerController target = coll.GetComponent<ULFollowerController> ();
				if (target.state == ULFollowerController.FollowerState.Boss) {
					AkSoundEngine.PostEvent ("TrumpGetPunch", ULGlobalSoundManager.instance);
					animator.Play ("HitBoss");
					Camera.main.gameObject.GetComponent<ULScreenShake> ().enabled = true;
					ULGameStateHandler.EndGame (false);
					isHugging = true;
					Destroy (coll.gameObject);
					Destroy (this);
				}
				isHugging = false;
			}
		}
		transform.Translate (GetAxis () * ULGlobals.playerSpeed * Time.fixedDeltaTime);

		halo.color = new Color (1.0f, 1.0f, 1.0f, Mathf.Clamp01 (ULFollowerController.gaiCount / ULGlobals.maxFollowers));
	}

	private void LateUpdate () {
		halo.sortingOrder = sr.sortingOrder - 1;
	}

	private Collider2D GetTarget () {
		/*
		Debug.DrawRay (transform.position + (toTheRight ? Vector3.right : Vector3.left) * hugDistance + Vector3.up * hugHalfBox.y + Vector3.left * hugHalfBox.x, Vector3.down * hugHalfBox.y * 2.0f, Color.magenta, 0.5f);
		Debug.DrawRay (transform.position + (toTheRight ? Vector3.right : Vector3.left) * hugDistance + Vector3.up * hugHalfBox.y + Vector3.left * hugHalfBox.x, Vector3.right * hugHalfBox.x * 2.0f, Color.magenta, 0.5f);
		Debug.DrawRay (transform.position + (toTheRight ? Vector3.right : Vector3.left) * hugDistance + Vector3.down * hugHalfBox.y + Vector3.right * hugHalfBox.x, Vector3.up * hugHalfBox.y * 2.0f, Color.magenta, 0.5f);
		Debug.DrawRay (transform.position + (toTheRight ? Vector3.right : Vector3.left) * hugDistance + Vector3.down * hugHalfBox.y + Vector3.right * hugHalfBox.x, Vector3.left * hugHalfBox.x * 2.0f, Color.magenta, 0.5f);//*/
		return Physics2D.OverlapBox (transform.position + (toTheRight ? Vector3.right : Vector3.left) * hugDistance, hugHalfBox, 0.0f, 1280);
		// Huggable && Handcuffed ((1 << 8) | (1 << 10))
	}

	private IEnumerator HugAnim (ULFollowerController fo) {
		if (fo.state == ULFollowerController.FollowerState.Handcuffed) {
			animator.Play ("Freedom");
			yield return new WaitForSeconds (ULGlobals.hugDuration);
		}
		else {
			this.sr.color = new Color (1.0f, 1.0f, 1.0f, fo.state == ULFollowerController.FollowerState.Boss ? 1f : 0f);
			yield return new WaitForSeconds (ULGlobals.hugDuration);
		}
		StartCoroutine ("Stop");
	}

	private IEnumerator Stop () {
		this.sr.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		yield return null;
	}


	protected override void CharFixedUpdate () {
		if (isHugging)
			return;
		base.CharFixedUpdate ();
	}

	private IEnumerator HugFollower (bool empty) {
		yield return new WaitForSeconds (empty ? ULGlobals.emptyHugDuration : ULGlobals.hugDuration);
		isHugging = false;
	}

	public void Push (Vector3 force) {
		transform.Translate (force);
	}

	protected override Vector3 GetAxis () {
		return new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")).normalized;
	}
}
