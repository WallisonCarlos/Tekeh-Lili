using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidePlatform : MonoBehaviour {
	[SerializeField]
	private BoxCollider2D platformCollider2D;
	[SerializeField]
	private BoxCollider2D platformTrigger2D;
	// Use this for initialization
	void Start () {
		Physics2D.IgnoreCollision (platformCollider2D, platformTrigger2D, true);
	}
	
	// Update is called once per frame
	private void OnTriggerEnter2D (Collider2D collision) {
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") {
			Physics2D.IgnoreCollision (platformCollider2D, collision, true);	
		}
	}

	private void OnTriggerExit2D (Collider2D collision) {
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") {
			Physics2D.IgnoreCollision (platformCollider2D, collision, false);
		}
	}
}
