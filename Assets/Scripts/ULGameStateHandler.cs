using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ULGameStateHandler : MonoBehaviour {
	private static ULGameStateHandler instance = null;
	public enum GameState {
		None,
		Menu,
		Credits,
		Game,
		Pause
	}

	public GameState state {
		get;
		private set;
	}

	public void SetState (GameState s) {
		switch (s) {
			case GameState.None:
				break;
			case GameState.Menu:
				break;
			case GameState.Credits:
				break;
			case GameState.Game:
				if (state != GameState.Pause)
					AkSoundEngine.SetState ("States_Zone", "Zone01");
				else
					AkSoundEngine.PostEvent ("Unpause", ULGlobalSoundManager.instance);
				break;
			case GameState.Pause:
				AkSoundEngine.PostEvent ("Pause", ULGlobalSoundManager.instance);
				break;
		}
		state = s;
	}

	private void Awake () {
		if (instance == null) {
			instance = this;
			gameObject.AddComponent<ULGlobalSoundManager> ();
			DontDestroyOnLoad (gameObject);
			AkSoundEngine.PostEvent ("StartGameMusic", ULGlobalSoundManager.instance);
			SetState (GameState.Game);
		}
		else
			Destroy (this);
	}

	public static void Reload () {
		AkSoundEngine.PostEvent ("YouLoose", ULGlobalSoundManager.instance);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		Debug.LogWarning ("Reload");
	}

	public static void Victory () {
		AkSoundEngine.PostEvent ("YouWin", ULGlobalSoundManager.instance);
		Debug.LogWarning ("Victory");
	}

	public static void Failed () {
		AkSoundEngine.PostEvent ("YouLoose", ULGlobalSoundManager.instance);
		Debug.LogWarning ("Failed");
	}
}
