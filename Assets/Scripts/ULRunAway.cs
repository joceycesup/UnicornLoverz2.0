using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULRunAway : MonoBehaviour {


	void Start () {
		gameObject.layer = 9; // run away
	}

	void Update () {
		transform.Translate (Vector3.left * ULGlobals.followerSpeed * Time.deltaTime);
	}
}
