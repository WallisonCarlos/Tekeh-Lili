using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour {

	private Player player;
	public float distance = 2f;
	private Rigidbody2D rb2D;
	public float speed = 200f;
	private bool wallJumping;
	// Use this for initialization
	void Start () {
		player = GetComponent<Player> ();
		rb2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Physics2D.queriesStartInColliders = false;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.right*(transform.localScale.x), distance);
		if (Input.GetKeyDown (KeyCode.Space) && !player.isFloor && hit.collider != null) {
			/*
			rb2D.velocity = new Vector2 (speed*hit.normal.x, speed);
			float scale = 0.25f;
			//player.velocidade = speed*hit.normal.x;
			transform.localScale = (transform.localScale.x == scale) ? new Vector2 (-scale, scale) : new Vector2 (scale, scale);
			//transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			*/
			wallJumping = true;
			Debug.Log (hit.normal.x);
			rb2D.velocity = new Vector2 (speed * hit.normal.x, speed);
			player.velocidade = speed * hit.normal.x;
		} else if (hit.collider != null && wallJumping){
			wallJumping = false;
		}
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.blue;
		Gizmos.DrawLine (transform.position, transform.position + Vector3.right * (transform.localScale.x) * distance);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if ((!wallJumping || player.isFloor)) {
			//player.velocidade = 0;
		}
	}
}
