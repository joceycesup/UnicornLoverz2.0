using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULFollowerController : ULCharacter {
	public enum FollowerState {
		CRS,
		Normal,
		Gai,
		Handcuffed,
		Down
	}

	public static float pushForce = 0.5f;

	public FollowerState state = FollowerState.Normal;

	void Start () {
		base.Init ();
	}

	private void ChangeSprite () {
		switch (state) {
			case FollowerState.CRS:
				break;
			case FollowerState.Normal:
				break;
			case FollowerState.Gai:
				break;
			case FollowerState.Handcuffed:
				break;
			case FollowerState.Down:
				break;
		}
	}

	public void Hugged (ULPlayerController player) {
		if (state == FollowerState.Normal) {
			state = FollowerState.Gai;
			ChangeSprite ();
		}
		else if (state == FollowerState.Handcuffed) {
			state = FollowerState.Gai;
			ChangeSprite ();
		}
		else if (state == FollowerState.CRS) {
			player.Push (Vector3.Normalize (player.transform.position - transform.position) * pushForce);
		}
	}

	protected override Vector3 GetAxis () {
		return Vector3.zero;
	}
}
