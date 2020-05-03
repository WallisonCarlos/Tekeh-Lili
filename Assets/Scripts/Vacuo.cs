using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuo : MonoBehaviour {

	[SerializeField]
	private BoxCollider2D vacuoCollider;

	private void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			Player player = Player.Instance.GetComponent<Player> ();
			player.Dead ();
		}
	}
}
