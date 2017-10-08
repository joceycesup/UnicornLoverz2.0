using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULCanvasController : MonoBehaviour {
	private static GameObject buttonHug;
	private static bool boolbuttonHug = false;
	private static GameObject buttonHit;
	private static bool boolbuttonHit = false;
	private static GameObject EndPanel;

	// Use this for initialization
	void Start () {
		buttonHug = GameObject.Find ("HugButton");
		buttonHit = GameObject.Find ("HitButton");
		buttonHug.SetActive (false);
		buttonHit.SetActive (false);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (boolbuttonHug) {
			if (ULFollowerController.gaiCount != 0) {
				buttonHug.SetActive (false);
				boolbuttonHug = false;
			}
		}
	}

	public static void ActiveButtonHug () {
		if (!boolbuttonHug && ULFollowerController.gaiCount == 0) {
			boolbuttonHug = true;
			buttonHug.SetActive (true);
		}
	}

	public static void ActiveButtonHit () {
		if (!boolbuttonHit) {
			boolbuttonHit = true;
			buttonHit.SetActive (true);
		}
	}
}
