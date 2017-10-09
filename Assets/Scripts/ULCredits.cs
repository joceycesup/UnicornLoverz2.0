using UnityEngine;
using UnityEngine.SceneManagement;

public class ULCredits : MonoBehaviour {

	public void credits () {
		ULGameStateHandler.SetState (ULGameStateHandler.GameState.Credits);
		SceneManager.LoadScene ("CREDITS");
	}
}
