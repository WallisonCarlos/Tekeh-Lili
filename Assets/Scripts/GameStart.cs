using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

	public Button yourButton;

	void Start()
	{
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		PlayerPrefs.DeleteKey ("dataPlayer");
		if (yourButton.tag == "Play") {
			SceneManager.LoadScene ("impress", LoadSceneMode.Single);
		} 
		if (yourButton.tag == "Quit") {
			Quit ();	
		}
	}

	private void Quit () {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
