using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULPlayerController : ULCharacter {
	public Vector2 hugHalfBox = new Vector2 (8.0f, 14.0f);
	public float hugDistance = 8.0f;
	private SpriteRenderer halo;
    private bool isHugging = false;

	protected override void Init () {
		base.Init ();
		halo = transform.GetChild (0).GetComponent<SpriteRenderer> ();
	}

	protected override void CharUpdate () {
        if (isHugging)
            return;
        base.CharUpdate();
        if (Input.GetButtonDown ("Hug")) {
			Collider2D coll = Physics2D.OverlapBox (transform.position + (toTheRight ? Vector3.right : Vector3.left) * hugDistance, hugHalfBox, 0.0f, 1280); // Huggable && Handcuffed ((1 << 8) | (1 << 10))

            isHugging = true;                                                                                                                                            /*			
																																							 Debug.DrawRay (transform.position + (toTheRight ? Vector3.right : Vector3.left) * hugDistance + Vector3.up * hugHalfBox.y + Vector3.left * hugHalfBox.x, Vector3.down * hugHalfBox.y * 2.0f, Color.magenta, 0.5f);
																																							 Debug.DrawRay (transform.position + (toTheRight ? Vector3.right : Vector3.left) * hugDistance + Vector3.up * hugHalfBox.y + Vector3.left * hugHalfBox.x, Vector3.right * hugHalfBox.x * 2.0f, Color.magenta, 0.5f);
																																							 Debug.DrawRay (transform.position + (toTheRight ? Vector3.right : Vector3.left) * hugDistance + Vector3.down * hugHalfBox.y + Vector3.right * hugHalfBox.x, Vector3.up * hugHalfBox.y * 2.0f, Color.magenta, 0.5f);
																																							 Debug.DrawRay (transform.position + (toTheRight ? Vector3.right : Vector3.left) * hugDistance + Vector3.down * hugHalfBox.y + Vector3.right * hugHalfBox.x, Vector3.left * hugHalfBox.x * 2.0f, Color.magenta, 0.5f);//*/
            if (coll != null) {
				coll.GetComponent<ULCharacter> ().Hugged (this);
                animator.Play("Hug");
			}
			else {
				animator.Play ("EmptyHug");
            }
            StartCoroutine(HugFollower(coll == null));
        }
		transform.Translate (GetAxis () * ULGlobals.playerSpeed * Time.fixedDeltaTime);

		halo.color = new Color (1.0f, 1.0f, 1.0f, Mathf.Clamp01 (ULFollowerController.gaiCount / ULGlobals.maxFollowers));
	}

    private IEnumerator HugFollower(bool empty)
    {
        yield return new WaitForSeconds(empty ? ULGlobals.emptyHugDuration : ULGlobals.hugDuration);
        isHugging = false;
    }

	public void Push (Vector3 force) {
		transform.Translate (force);
	}

	protected override Vector3 GetAxis () {
		return new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")).normalized;
	}
}
