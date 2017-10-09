using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULTutoTriggerCollider : MonoBehaviour {
	public bool hug;
	private BoxCollider2D co = null;
	private bool canTrigger = true;

	private void Awake () {
		Init ();
	}

	private void OnTriggerEnter2D (Collider2D collision) {
		if (!canTrigger)
			return;
		if (collision.name == "Character") {
			if (hug)
				ULCanvasController.ActiveButtonHug ();
			else {
				ULCanvasController.ActiveButtonHit ();
				AkSoundEngine.PostEvent ("TrumpLol", ULGlobalSoundManager.instance);
				ULCrowdControl.AllRunAway ();
			}
			canTrigger = false;
		}
	}

	public void Init () {
		canTrigger = true;
	}
}
