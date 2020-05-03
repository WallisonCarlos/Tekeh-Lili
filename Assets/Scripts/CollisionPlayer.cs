using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlayer : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private Collider2D outro;

	private void Awake () {
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), outro, true);
	}

	void OnCollisionEnter2D (Collision2D collision) {
		GameObject o = collision.gameObject;
		if (o.tag == "Player" || o.tag == "Enemy") {
			Physics2D.IgnoreCollision (GetComponent<Collider2D> (), collision.collider, true);
		}
	}
}
