using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULSpritesSorter : MonoBehaviour {
	public bool isStatic = false;
	private SpriteRenderer sr;
	private Collider2D coll;

	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		coll = GetComponentInChildren<Collider2D> ();
		Resort ();
		if (isStatic) {
			transform.localPosition = new Vector3 (Mathf.Round (transform.localPosition.x), Mathf.Round (transform.localPosition.y));
			Destroy (this);
		}
	}

	private void Update () {
		Resort ();
	}

	void Resort () {
		float y = coll.bounds.center.y;
		sr.sortingOrder = -(int) ((y / ULGlobals.mapHalfHeight) * 8192.0f);
	}
}
