using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULMilitiaHandler : MonoBehaviour {
	public ULFence startFence;
	public ULFence stopFence;

	void Start () {
		startFence.OnOpen += ReleaseMilitia;
		stopFence.OnOpen += StopMilitia;
	}

	void ReleaseMilitia () {
		foreach (ULMilitia militia in transform.GetComponentsInChildren<ULMilitia> ()) {
			militia.enabled = true;
		}
	}

	void StopMilitia () {
		foreach (ULMilitia militia in transform.GetComponentsInChildren<ULMilitia> ()) {
			militia.RunAway ();
		}
		Destroy (this);
	}
}
