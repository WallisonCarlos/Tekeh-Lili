using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreMasks : MonoBehaviour {
	[SerializeField]
	private Image scoreMasks;
	[SerializeField]
	private Sprite levelTwo;
	[SerializeField]
	private Sprite levelTree;
	[SerializeField]
	private Sprite end;
	// Use this for initialization
	
	// Update is called once per frame
	void Start () {
		Scene scene = SceneManager.GetActiveScene ();
		if (scene.buildIndex >= 4 && scene.buildIndex < 5) {
			scoreMasks.sprite = levelTwo;
		} else if (scene.buildIndex >= 6 && scene.buildIndex < 8) {
			scoreMasks.sprite = levelTree;
		} else if (scene.buildIndex >= 8) {
			scoreMasks.sprite = end;
		}
	}
}
