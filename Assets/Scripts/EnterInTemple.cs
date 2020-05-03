using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterInTemple : MonoBehaviour {

	[SerializeField]
	private string scene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D outro) {
		if (outro.gameObject.tag == "Player") {

			PlayerPrefs.SetInt ("dataPlayer", 1);
			Life life = Player.Instance.GetComponent <Life> ();
			PlayerPrefs.SetFloat ("vidaPersonagem", life.vidaPersonagem);
			PlayerPrefs.SetInt ("score", Player.Instance.score);
			PlayerPrefs.SetInt ("kunaiCount", Player.Instance.kunaiCount);
			PlayerPrefs.SetInt ("shurikenCount", Player.Instance.shurikenCount);
			PlayerPrefs.Save ();
			/*
			Debug.Log (scene);
			SceneManager.LoadSceneAsync (scene, LoadSceneMode.Single);
			Scene s = SceneManager.GetSceneByName (scene);
			if (s.IsValid ()) {
				SceneManager.MoveGameObjectToScene (outro.gameObject, s);
				SceneManager.LoadScene (scene, LoadSceneMode.Single);
			} else {
				Debug.Log ("Scene invalid!");
			}*/
			SceneManager.LoadScene (scene, LoadSceneMode.Single);
		}
	}
}
