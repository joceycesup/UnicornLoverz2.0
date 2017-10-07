using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULFollower : MonoBehaviour {
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

	}

	void Update () {

	}

	private void ChangeSprite () {

	}

	public void Hugged (ULCharacterController player) {
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
}
