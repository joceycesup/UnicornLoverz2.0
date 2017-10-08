using UnityEngine;

public class ULScreenShake : MonoBehaviour {
	private float localX;
	private float localY;
	public float shakeRadius;
	public float shakeSpeed;

	private Vector3 direction;

	private void Start () {
		NewDirection (0.0f);
	}
	//*
	void Update () {
		transform.localPosition += direction * Time.deltaTime;
		if (transform.localPosition.x > shakeRadius) {
			transform.localPosition = new Vector3 (shakeRadius, transform.localPosition.y);
			NewDirection (0.0f);
		}
		else if (transform.localPosition.x < -shakeRadius) {
			transform.localPosition = new Vector3 (-shakeRadius, transform.localPosition.y);
			NewDirection (180.0f);
		}
		if (transform.localPosition.y > shakeRadius) {
			transform.localPosition = new Vector3 (transform.localPosition.x, shakeRadius);
			NewDirection (90.0f);
		}
		else if (transform.localPosition.y < -shakeRadius) {
			transform.localPosition = new Vector3 (transform.localPosition.x, -shakeRadius);
			NewDirection (-90.0f);
		}
	}//*/

	void NewDirection (float baseAngle) {
		direction = Quaternion.Euler (0.0f, 0.0f, baseAngle + Random.Range (91.0f, 269.0f)) * Vector3.right * shakeSpeed;
	}
}
