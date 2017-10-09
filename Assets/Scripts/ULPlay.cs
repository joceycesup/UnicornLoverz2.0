using UnityEngine.SceneManagement;
using UnityEngine;

public class ULPlay : MonoBehaviour {
	
	public void jouer() {
		ULGameStateHandler.SetState (ULGameStateHandler.GameState.Game);
        SceneManager.LoadScene("MainNoemie");
	}
}
