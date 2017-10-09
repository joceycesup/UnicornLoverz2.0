using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ULCharacter : MonoBehaviour {
	public bool toTheRight = true;
	protected SpriteRenderer _sr;
	public SpriteRenderer sr {
		get { return _sr; }
		protected set { _sr = value; }
	}
	protected Animator _animator;
	public Animator animator {
		get { return _animator; }
		protected set { _animator = value; }
	}

	protected virtual void Init () {
		if (GetComponent<ULSpritesSorter> () == null)
			gameObject.AddComponent<ULSpritesSorter> ();
		sr = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
	}

	void Start () {
		Init ();
	}

	protected virtual void CharUpdate () {
	}

	private void Update () {
		if (ULGameStateHandler.state == ULGameStateHandler.GameState.Pause)
			return;
		CharUpdate ();
	}

	protected virtual void CharFixedUpdate () {
		Vector3 direction = GetAxis ();
		if (direction.x != 0.0f) {
			if (toTheRight != (toTheRight = direction.x > 0.0f)) {
				sr.flipX = !toTheRight;
			}
		}
		if (direction.x != 0.0f || direction.y != 0.0f) {
			animator.Play ("Walk");
		}
		else {
			animator.Play ("Idle");
		}
	}

	void FixedUpdate () {
		if (ULGameStateHandler.state == ULGameStateHandler.GameState.Pause)
			return;
		CharFixedUpdate ();
	}

	public virtual bool Hugged (ULPlayerController player) {
		return false;
	}

	public virtual void RunAway () {
		animator.Play ("Walk");
		sr.flipX = true;
		gameObject.AddComponent<ULRunAway> ();
		Destroy (this);
	}

	protected abstract Vector3 GetAxis ();
}
