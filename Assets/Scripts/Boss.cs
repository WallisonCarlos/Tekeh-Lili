using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Boss : MonoBehaviour {
	public string scene;
	private LifeEnemy life;
	// Use this for initialization
	void Start () {
		life = GetComponent<LifeEnemy> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (life.curHealth <= 0) {

			PlayerPrefs.SetInt ("dataPlayer", 1);
			Life life = Player.Instance.GetComponent <Life> ();
			PlayerPrefs.SetFloat ("vidaPersonagem", life.vidaPersonagem);
			PlayerPrefs.SetInt ("score", Player.Instance.score);
			PlayerPrefs.SetInt ("kunaiCount", Player.Instance.kunaiCount);
			PlayerPrefs.SetInt ("shurikenCount", Player.Instance.shurikenCount);
			PlayerPrefs.Save ();
			SceneManager.LoadScene (scene, LoadSceneMode.Single);
		}
	}
}
