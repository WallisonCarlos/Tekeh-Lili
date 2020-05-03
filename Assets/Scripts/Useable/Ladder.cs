using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ladder : MonoBehaviour, IUseable {

	[SerializeField]
	private Collider2D platformCollider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Use () {
		if(Player.Instance.onLadder){
			//We need to stop climbing
			UseLadder (false, 1, 0, 1);
		}else{
			//We need to start climbing
			UseLadder (true, 0, 1, 0);
			//Debug.Log ("Ignore");
			Physics2D.IgnoreCollision (Player.Instance.GetComponent<Collider2D> (), platformCollider, true);
		}
	}

	private void UseLadder (bool onLadder, int gravity, int layerWeight, int animSpeed) {
		Player.Instance.onLadder = onLadder;
		Player.Instance.rb2D.gravityScale = gravity;
		Player.Instance.animator.SetLayerWeight (3, layerWeight);
		Player.Instance.animator.speed = animSpeed;
	}

	private void OnTriggerExit2D (Collider2D outro) {
		if (outro.tag == "Player") {
			UseLadder (false, 1, 0, 1);
			//Debug.Log ("Not Ignore");
			Physics2D.IgnoreCollision (Player.Instance.GetComponent<Collider2D> (), platformCollider, false);
		}		
	}
}
