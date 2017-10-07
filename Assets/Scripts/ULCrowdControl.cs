using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULCrowdControl : MonoBehaviour {

    private GameObject Player;
    private Vector3 PreviousPosition;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        PreviousPosition = Player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator CheckPlayerPosition(){
        PreviousPosition = Player.transform.position;
        yield return new WaitForSeconds(.1f);
    }
}
