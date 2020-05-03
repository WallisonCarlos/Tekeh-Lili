using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Impress : MonoBehaviour {

	private int state = 0;
	public Button yourButton;
	public Sprite  sprite;
	public Image image;
	void Start() {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		if (state == 1) {	
			SceneManager.LoadScene ("levelOne", LoadSceneMode.Single);
		} else {
			image.sprite = sprite;
			state = 1;
		}
	}
}
