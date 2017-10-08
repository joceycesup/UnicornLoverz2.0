using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULGameStateHandler : MonoBehaviour {
	private static ULGameStateHandler instance = null;

	private void Awake () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else
			Destroy (this);
	}

	public static void Reload () {
		Debug.LogError ("Reload");
		Debug.Break ();
	}

	public static void Victory () {
		Debug.LogError ("Victory");
		Debug.Break ();
	}

	public static void Failed () {
		Debug.LogError ("Failed");
		Debug.Break ();
	}
}
