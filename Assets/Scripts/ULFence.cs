using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULFence : MonoBehaviour {
	public Sprite closed;
	public Sprite openable;
	public Sprite open;
	public int minCrowd = 20;
	protected SpriteRenderer sr;
	public delegate void OpenEvent();
	public OpenEvent OnOpen;

	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}

	void Update () {
		if(ULFollowerController.gaiCount >= minCrowd) {
			sr.sprite = openable;
		}
		else {
			sr.sprite = closed;
		}
	}

	private void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.GetComponent<ULPlayerController>() != null && ULFollowerController.gaiCount >= minCrowd) {
			Destroy (GetComponent<BoxCollider2D> ());
			StartCoroutine ("Open");
		}
	}

	private IEnumerator Open () {
		yield return null;
		sr.sprite = open;
		if (OnOpen != null)
			OnOpen ();
		Destroy (this);
	}
}
