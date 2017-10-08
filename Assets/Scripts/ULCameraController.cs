using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULCameraController : MonoBehaviour {
	public float minSize = 1.0f;

	void Update () {
		if (ULGlobals.player == null) {
			Destroy (this);
			return;
		}
		Camera.main.orthographicSize = Mathf.Lerp (minSize, ULGlobals.mapHalfHeight, ULFollowerController.gaiCount / ULGlobals.maxFollowers);
		float y = Mathf.Clamp (ULGlobals.player.transform.position.y, -ULGlobals.mapHalfHeight + Camera.main.orthographicSize, ULGlobals.mapHalfHeight - Camera.main.orthographicSize);
		transform.position = new Vector3 (ULGlobals.player.transform.position.x, y, transform.position.z);
	}
}
