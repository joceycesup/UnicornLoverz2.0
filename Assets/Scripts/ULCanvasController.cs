using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULCanvasController : MonoBehaviour {
	private static GameObject buttonHug;
	private static GameObject buttonHit;
	private static GameObject endPanel;
	
	void Start () {
		buttonHug = GameObject.Find ("HugButton");
		buttonHit = GameObject.Find ("HitButton");
		buttonHug.SetActive (false);
		buttonHit.SetActive (false);
	}
	
	void Update () {
		if (buttonHug.activeInHierarchy) {
			if (ULFollowerController.gaiCount != 0) {
				buttonHug.SetActive (false);
			}
		}
		if (buttonHit.activeInHierarchy) {
			if (ULGameStateHandler.state == ULGameStateHandler.GameState.End) {
				buttonHit.SetActive (false);
			}
		}
	}

	public static void ActiveButtonHug () {
		if (!buttonHug.activeInHierarchy && ULFollowerController.gaiCount == 0) {
			buttonHug.SetActive (true);
		}
	}

	public static void ActiveButtonHit () {
		if (!buttonHit.activeInHierarchy) {
			buttonHit.SetActive (true);
		}
	}
}
