using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULGlobals : MonoBehaviour {
	private static ULGlobals instance = null;

	private ULPlayerController _player = null;
	public static ULPlayerController player { get { return instance._player; } set { instance._player = value; } }
	public int _maxFollowers = 100;
	public static int maxFollowers { get { return instance._maxFollowers; } }
	public float _militiaPushForce = 0.5f;
	public static float militiaPushForce { get { return instance._militiaPushForce; } }
	public float _militiaChooseRadius = 25.0f;
	public static float militiaChooseRadius { get { return instance._militiaChooseRadius; } }

	public float _iaLocalRadius = 5.0f;
	public static float iaLocalRadius { get { return instance._iaLocalRadius; } }
	public float _iaSpeedMax = 5.0f;
	public static float iaSpeedMax { get { return instance._iaSpeedMax; } }

	public float _mapHalfHeight = 181.5f;
	public static float mapHalfHeight { get { return instance._mapHalfHeight; } }

	public float _handcuffCountDown = 5.0f;
	public static float handcuffCountDown { get { return instance._handcuffCountDown; } }
	public float _handcuffRadius = 5.0f;
	public static float handcuffRadius { get { return instance._handcuffRadius; } }

	private Transform _followersGroup = null;
	public static Transform followersGroup { get { return instance._followersGroup; } private set { instance._followersGroup = value; } }
	public float _iaDistanceThreshold = 10.0f;
	public static float iaDistanceThreshold { get { return instance._iaDistanceThreshold; } }

    public RuntimeAnimatorController[] _animatorListHappy;
    public static RuntimeAnimatorController[] animatorListHappy { get { return instance._animatorListHappy; } }

    public float _playerSpeed = 25.0f;
	public static float playerSpeed { get { return instance._playerSpeed; } }
	public float _militiaSpeed = 25.0f;
	public static float militiaSpeed { get { return instance._militiaSpeed; } }
    public float _followerSpeed = 25.0f;
    public static float followerSpeed { get { return instance._followerSpeed; } }

    public float _hugDuration = 5.0f;
    public static float hugDuration { get { return instance._hugDuration; } }
    public float _emptyHugDuration = 5.0f;
    public static float emptyHugDuration { get { return instance._emptyHugDuration; } }

	public Vector2 _randomConvertSound = new Vector2 (5.0f, 20.0f);
	public static Vector2 randomConvertSound { get { return instance._randomConvertSound; } }
	public Vector2 _randomCRSSound = new Vector2 (5.0f, 10.0f);
	public static Vector2 randomCRSSound { get { return instance._randomCRSSound; } }

	private void Awake () {
		if (instance == null) {
			instance = this;
			player = FindObjectOfType<ULPlayerController> ();
			followersGroup = FindObjectOfType<ULCrowdControl> ().transform;
			followersGroup.position = player.transform.position;
			DontDestroyOnLoad (gameObject);
		}
		else
			Destroy (this);
	}
}
