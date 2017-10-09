using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULFenceBearer : MonoBehaviour {
	private ULFence fence;
	protected Animator animator;
	private GUIStyle guiStyle = new GUIStyle ();
	public float fontSize = 0.2f;
	public Font font;
	public Vector2 textOffset = new Vector2 (-4.5f, 11.5f);

	void Start () {
		fence = transform.parent.GetComponent<ULFence> ();
		animator = GetComponent<Animator> ();
	}

	void Update () {/*
		if (ULFollowerController.gaiCount >= fence.minCrowd) {
			// Lauch anim
		}
		else {
			// Stop anim
		}//*/
	}

	private void OnGUI () {
		Vector3 pos = Camera.main.WorldToScreenPoint (new Vector3 (transform.position.x - textOffset.x, transform.position.y + textOffset.y));/*
		float zoom = ULGlobals.mapHalfHeight / Camera.main.orthographicSize;
		float ratio = ((float) Camera.main.pixelHeight) / ULGlobals.mapHalfHeight;/*/
		float ratio = ((float) Camera.main.pixelHeight) / Camera.main.orthographicSize;//*/
		guiStyle.fontSize = (int) (8.0f * ratio * fontSize);
		guiStyle.font = font;
		guiStyle.normal.textColor = Color.white;
		GUI.Label (new Rect (pos.x, Screen.height - pos.y, 9.0f * ratio, 8.0f * ratio), fence.minCrowd.ToString("000"), guiStyle);
	}
}
