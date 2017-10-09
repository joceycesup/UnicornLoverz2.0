using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ULRetour : MonoBehaviour {

	public void Retour () {
		SceneManager.LoadScene ("START SCREEN");
		ULGameStateHandler.SetState (ULGameStateHandler.GameState.Menu);
	}
}
