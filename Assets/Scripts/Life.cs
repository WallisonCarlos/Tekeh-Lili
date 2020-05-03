using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {
	[SerializeField]
	public float vidaPersonagem;
	[SerializeField]
	private Texture barraDeVida;
	[SerializeField]
	private Texture contorno;
	[SerializeField]
	private int vidaCheia = 100;
	// Use this for initialization
	void Start () {
		vidaPersonagem = vidaCheia;
	}
	
	// Update is called once per frame
	void Update () {
		if (vidaPersonagem >= vidaCheia) {
			vidaPersonagem = vidaCheia;			
		} else if (vidaPersonagem <= 0) {
			vidaPersonagem = 0;
		}
	}

	void OnGUI () {
		GUI.DrawTexture (new Rect (Screen.width/25, Screen.height/15, Screen.width/5.5f/vidaCheia*vidaPersonagem, Screen.height/25), barraDeVida);
		GUI.DrawTexture (new Rect (Screen.width/40, Screen.height/40, Screen.width/5, Screen.height/8), contorno);
	}

	public float GetVidaPersonagem () {
		return this.vidaPersonagem;
	}

	public void SetVidaPersonagem (float vidaPersonagem) {
		this.vidaPersonagem = vidaPersonagem;
	}

	public void PlusLife (float more) {
		if (vidaPersonagem + more >= vidaCheia) {
			vidaPersonagem = vidaCheia;			
		} else {
			vidaPersonagem += more;
		}
	}
}
