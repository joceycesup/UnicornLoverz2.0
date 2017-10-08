using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULTutoTriggerCollider : MonoBehaviour {
	public bool hug;

	private void OnTriggerEnter2D (Collider2D collision) {
		if (collision.name == "Character") {
			if (hug)
				ULCanvasController.ActiveButtonHug ();
			else {
				ULCanvasController.ActiveButtonHit ();
				AkSoundEngine.PostEvent ("TrumpLol", ULGlobalSoundManager.instance);
			}
			Destroy (this);
		}
	}
}
