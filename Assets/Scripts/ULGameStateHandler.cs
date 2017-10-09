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
		Pause,
		End
	}

	public static GameState state {
		get { return instance._state; }
		private set { instance._state = value; }
	}
	public GameState _state;

	public static void SetState (GameState s) {
		instance.SetGameState (s);
	}

	public void SetGameState (GameState s) {
		switch (s) {
			case GameState.None:
				break;
			case GameState.Menu:
				Cursor.visible = true;
				if (state == GameState.End)
					AkSoundEngine.PostEvent ("StartGameMusic", ULGlobalSoundManager.instance);
				AkSoundEngine.SetState ("States_Zone", "Zone01");
				break;
			case GameState.Credits:
				Cursor.visible = true;
				break;
			case GameState.Game:
				SetPause (false);
				if (state != GameState.Pause) {
					ULGlobals.Init ();
					AkSoundEngine.SetState ("States_Zone", "Zone01");
				}
				else
					AkSoundEngine.PostEvent ("Unpause", ULGlobalSoundManager.instance);
				break;
			case GameState.Pause:
				SetPause (true);
				AkSoundEngine.PostEvent ("Pause", ULGlobalSoundManager.instance);
				break;
		}
		state = s;
	}

	private void SetPause (bool show) {
		Cursor.visible = show;
		ULGlobals.UIList[2].gameObject.SetActive (show);
		Time.timeScale = show?0.0f:1.0f;
	}

	private void Awake () {
		if (instance == null) {
			instance = this;
			gameObject.AddComponent<ULGlobalSoundManager> ();
			AkSoundEngine.PostEvent ("StartGameMusic", ULGlobalSoundManager.instance);
			DontDestroyOnLoad (gameObject);
			SetState (_state);
		}
		else
			Destroy (this);
	}

	private void Update () {
		if (Input.GetButtonDown ("Escape")) {
			switch (state) {
				case GameState.None:
					break;
				case GameState.Menu:
					Application.Quit ();
					break;
				case GameState.Credits:
					SceneManager.LoadScene ("START SCREEN");
					break;
				case GameState.Game:
					SetState (GameState.Pause);
					break;
				case GameState.Pause:
					SetState (GameState.Game);
					break;
			}
		}
	}

	public static void Reload () {
		AkSoundEngine.PostEvent ("YouLoose", ULGlobalSoundManager.instance);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		Debug.LogWarning ("Reload");
		SetState (GameState.Game);
	}

	public static void EndGame (bool victory) {
		instance.StartCoroutine (StartEnd (victory));
	}

	private static IEnumerator StartEnd (bool victory) {
		yield return new WaitForSeconds (ULGlobals.endDelay);
		if (victory)
			Victory ();
		else
			Failed ();
		yield return new WaitForSeconds (ULGlobals.backToMenuDelay);
		SetState (GameState.Menu);
		SceneManager.LoadScene ("START SCREEN");
	}

	private static void Victory () {
		SetState (GameState.End);
		AkSoundEngine.PostEvent ("YouWin", ULGlobalSoundManager.instance);
		ULGlobals.UIList[1].transform.parent.gameObject.SetActive (true);
		ULGlobals.UIList[1].gameObject.SetActive (true);
		Debug.LogWarning ("Victory");
	}

	private static void Failed () {
		SetState (GameState.End);
		AkSoundEngine.PostEvent ("YouLoose", ULGlobalSoundManager.instance);
		ULGlobals.UIList[0].transform.parent.gameObject.SetActive (true);
		ULGlobals.UIList[0].gameObject.SetActive (true);
		Debug.LogWarning ("Failed");
	}
}
