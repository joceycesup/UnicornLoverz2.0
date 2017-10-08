﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULFence : MonoBehaviour {
	public int minCrowd = 20;
	public delegate void OpenEvent ();
	public OpenEvent OnOpen;
	private bool isOpen = false;

	void Update () {
		if (ULFollowerController.gaiCount >= minCrowd) {
		}
		else {
		}
	}

	private void OnCollisionEnter2D (Collision2D collision) {
		if (isOpen)
			return;
		if (collision.gameObject.GetComponent<ULPlayerController> () != null && ULFollowerController.gaiCount >= minCrowd) {
			isOpen = true;
			StartCoroutine ("Open");
		}
	}

	private IEnumerator Open () {
		transform.GetChild (2).gameObject.SetActive (true);
		Destroy (transform.GetChild (0).gameObject);
		Destroy (transform.GetChild (1).gameObject);
		if (OnOpen != null)
			OnOpen ();
		Destroy (this);
		yield return null;
	}
}
