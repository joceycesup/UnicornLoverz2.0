using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ULCanvasController : MonoBehaviour {
	private static GameObject buttonHug { get { return ULGlobals.UIList[3]; } }
	private static GameObject buttonHit { get { return ULGlobals.UIList[4]; } }
	private static GameObject hugCollider { get { return ULGlobals.UIList[5]; } }
	private static GameObject hitCollider { get { return ULGlobals.UIList[6]; } }

	private void Start () {
		Init ();
	}

	public static void Init () {
		buttonHug.SetActive (false);
		buttonHit.SetActive (false);
		hugCollider.GetComponent<ULTutoTriggerCollider> ().Init ();
		hitCollider.GetComponent<ULTutoTriggerCollider> ().Init ();
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

	private void Restart () {
		ULGameStateHandler.Reload ();
	}

	private void Exit () {
		SceneManager.LoadScene ("START SCREEN");
		ULGameStateHandler.SetState (ULGameStateHandler.GameState.Menu);
	}
}
