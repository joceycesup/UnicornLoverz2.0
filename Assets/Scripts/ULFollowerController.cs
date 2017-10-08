using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULFollowerController : ULCharacter {
	public enum FollowerState {
		Normal,
		Gai,
		Handcuffed,
		Down
	}

	public static int gaiCount {
		get { return _gaiCount; }
		protected set { _gaiCount = value; }
	}
	protected static int _gaiCount = 0;

	public FollowerState state = FollowerState.Normal;

	protected override void Init () {
		base.Init ();
		localOrigin = transform.localPosition;
	}

	private void ChangeSprite () {
		switch (state) {
			case FollowerState.Normal: // A virer ?
				break;
			case FollowerState.Gai:
				this.animator.runtimeAnimatorController = ULGlobals.animatorListHappy[Random.Range (0, ULGlobals.animatorListHappy.Length)];
				break;
			case FollowerState.Handcuffed:
				this.animator.Play ("Handcuffed");
				break;
			case FollowerState.Down:
				break;
		}
	}

	public override void Hugged (ULPlayerController player) {
		if (state == FollowerState.Normal) {
			state = FollowerState.Gai;
			followedGroup = ULGlobals.followersGroup;
			transform.parent = followedGroup;
			Debug.Log ("Hugged " + transform);
			if (gaiCount == 0)
				followedGroup.position = transform.position;
			localOrigin = Vector3.zero;
			gaiCount++;
			ChangeSprite ();
		}
		else if (state == FollowerState.Handcuffed) {
			StopCoroutine ("HandcuffCountDown");
			gameObject.layer = 8; // Huggable
			state = FollowerState.Gai;
			transform.parent = followedGroup;
			ChangeSprite ();
		}
	}

	public void Handcuff (ULCharacter militia) {
		if (state != FollowerState.Handcuffed) {
			gameObject.layer = 10; // Handcuffed
			transform.parent = null;
			state = FollowerState.Handcuffed;
			StartCoroutine ("HandcuffCountDown");
		}
	}

	private IEnumerator HandcuffCountDown () {
		yield return new WaitForSeconds (ULGlobals.handcuffCountDown);
		state = FollowerState.Down;
		gaiCount--;
		transform.parent = null;
		gameObject.layer = 0;
	}

	// ----- ----- -----
	private Transform followedGroup;

	private Vector3 localOrigin;
	private float localX;
	private float localY;

	protected override Vector3 GetAxis () {
		return new Vector3 (localX, localY);
	}

	private float directionDelay;
	private float speedMod = 1f;
	public float speedModMax = 5f;

	protected override void CharFixedUpdate () {
		if (state == FollowerState.Handcuffed || state == FollowerState.Down)
			return;

		if (followedGroup != null) {
			if ((this.transform.position - followedGroup.position).magnitude > ULGlobals.iaDistanceThreshold)
				speedMod = speedModMax;
			else
				speedMod = 1f;
		}
		directionDelay += Time.deltaTime;

		if (transform.localPosition.x > localOrigin.x + ULGlobals.iaLocalRadius) {
			localX = Random.Range (-ULGlobals.iaSpeedMax * speedMod, 0.0f);
			directionDelay = 0.0f;
		}
		else if (transform.localPosition.x < localOrigin.x - ULGlobals.iaLocalRadius) {
			localX = Random.Range (0.0f, ULGlobals.iaSpeedMax * speedMod);
			directionDelay = 0.0f;
		}
		if (transform.localPosition.y > localOrigin.y + ULGlobals.iaLocalRadius) {
			localY = Random.Range (-ULGlobals.iaSpeedMax * speedMod, 0.0f);
			directionDelay = 0.0f;
		}
		else if (transform.localPosition.y < localOrigin.y - ULGlobals.iaLocalRadius) {
			localY = Random.Range (0.0f, ULGlobals.iaSpeedMax * speedMod);
			directionDelay = 0.0f;
		}

		if (directionDelay > 1.0f) {
			localX = Random.Range (-ULGlobals.iaSpeedMax, ULGlobals.iaSpeedMax);
			localY = Random.Range (-ULGlobals.iaSpeedMax, ULGlobals.iaSpeedMax);
			directionDelay = 0.0f;
		}
		base.CharFixedUpdate ();

		//transform.localPosition = new Vector3 (transform.localPosition.x + localX, transform.localPosition.y + localY, transform.localPosition.z) * Time.fixedDeltaTime;
		transform.localPosition += GetAxis () * Time.fixedDeltaTime;
	}
}
