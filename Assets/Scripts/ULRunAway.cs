using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULRunAway : MonoBehaviour {

	void Awake () {
		gameObject.layer = 9; // run away
		transform.parent = null;
	}

	void Update () {
		if (ULGameStateHandler.state == ULGameStateHandler.GameState.Pause)
			return;
		transform.Translate (Vector3.left * ULGlobals.followerSpeed * Time.deltaTime);
	}
}
