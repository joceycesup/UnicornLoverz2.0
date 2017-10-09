using UnityEngine;

public class ULGlobalSoundManager : MonoBehaviour {
	private static GameObject _instance;
	public static GameObject instance {
		get;
		private set;
	}

	void Awake () {
		if (instance == null) {
			instance = gameObject;
			DontDestroyOnLoad (gameObject);
		}
		//else Destroy (gameObject);
	}
}