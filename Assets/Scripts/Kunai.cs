using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour {
	[SerializeField]
	private float velocidade = 200f;
	private Vector2 direcao;
	private Rigidbody2D rb2D;
	private bool ladoDireito;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb2D.velocity = direcao * velocidade;
	}

	void OnBecameInvisible () {
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D outro) {
		if (outro.gameObject.tag == "Enemy") {
			Destroy (gameObject);
		}

		if (outro.gameObject.tag == "Player") {
			
		}

		if (outro.gameObject.tag == "EnemyWeapon") {
			Destroy (gameObject);
		}

		if (outro.gameObject.tag == "PlayerWeapon") {
			Destroy (gameObject);
		}
	}

	public void Inicializar(Vector2 dir) {
		direcao = dir;
		ladoDireito = transform.localScale.x > 0;
		if (direcao.x > 0 && !ladoDireito || direcao.x<0 && ladoDireito) {
			ladoDireito = !ladoDireito;
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}
}
