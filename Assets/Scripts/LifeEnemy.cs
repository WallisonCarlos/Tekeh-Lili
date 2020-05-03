using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEnemy : MonoBehaviour {

	public float maxHealth = 100;
	public float curHealth = 100;

	public Texture2D healthBar;

	private float left;
	private float top;

	private Vector2 playerScreen;
	public float height = 1.17f;
	public float width = 0f;
	public Player player;
	public Enemy target;
	public float healthPercent;

	void Start () 
	{
		maxHealth = 10;
		curHealth = maxHealth;
	}

	void Update ()
	{
		if(gameObject != null) {
			target = gameObject.GetComponent<Enemy>();
			healthPercent = (float) curHealth / (float) maxHealth;
		} else {
			target = null;
			healthPercent = 0;
		}
		Vector3 healthBarWorldPosition = gameObject.transform.position + new Vector3(width, height, 0.0f);
		healthBarWorldPosition = Camera.main.WorldToScreenPoint(healthBarWorldPosition);
		//playerScreen = Camera.main.WorldToScreenPoint(target.transform.position);
		left = healthBarWorldPosition.x;                   //pretty sure right here
		top = (Screen.height - healthBarWorldPosition.y);  //is the issue
	}

	void OnGUI()
	{
		if (target != null) {
			GUI.DrawTexture(new Rect(left, top, (50 * healthPercent), 5), healthBar);
		}
	}
}
