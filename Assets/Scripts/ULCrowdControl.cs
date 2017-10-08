using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULCrowdControl : MonoBehaviour {
	private Vector3 lastPosition;
	private Queue<Vector3> previousPositions = new Queue<Vector3> ();
	public float pathfindingGranularity = 0.5f;
	private Vector3 tmpPosition;
	private int cptPos = 0;

	// Use this for initialization
	void Start () {
		lastPosition = ULGlobals.player.transform.position;
		previousPositions.Enqueue (lastPosition);
		StartCoroutine ("CheckPlayerPosition");
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (previousPositions.Count > 1 && cptPos % 2 == 0) {
			tmpPosition = previousPositions.Dequeue ();
		}
		Vector3 direction = tmpPosition - transform.position;
		float distance = direction.magnitude;
		direction = Vector3.Normalize (direction) * ULGlobals.followerSpeed * Time.fixedDeltaTime;
		if (direction.magnitude > distance)
			direction = Vector3.ClampMagnitude (direction, distance);
		transform.Translate (direction);
	}

	IEnumerator CheckPlayerPosition () {
		yield return new WaitForSeconds (pathfindingGranularity);

		Vector3 temp = (lastPosition - ULGlobals.player.transform.position);

		if (temp.magnitude > 1) {
			cptPos++;
			lastPosition = ULGlobals.player.transform.position;
			previousPositions.Enqueue (lastPosition);
		}
		StartCoroutine ("CheckPlayerPosition");
	}
}
