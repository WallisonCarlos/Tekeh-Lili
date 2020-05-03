using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusLife : MonoBehaviour {

	[SerializeField]
	private BoxCollider2D collider;

	void OnTrigerEnter2D (Collider2D outro) {
		if (outro.gameObject.tag == "Player") {
			Life life = Player.Instance.GetComponent<Life> ();
			life.vidaPersonagem += 20;
			Debug.Log ("Player plus life");
		}
	}
}
