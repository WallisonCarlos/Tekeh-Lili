using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Coin : MonoBehaviour {

	[SerializeField]
	private Text count;
	// Use this for initialization

	public void SetCountCoins() {
		Player.Instance.score = Player.Instance.score + 1; 
		count.text = Player.Instance.score.ToString ();
		Life life = Player.Instance.GetComponent<Life> ();
		if (Player.Instance.score % 10 == 0) {
			life.vidaPersonagem += 10;
		} 
	}
}
