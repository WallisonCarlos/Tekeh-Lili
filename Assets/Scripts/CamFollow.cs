using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

	[SerializeField]
	private float minX;
	[SerializeField]
	private float maxX;
	[SerializeField]
	private float minY;
	[SerializeField]
	private float maxY;

	[SerializeField]
	private Transform player;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (Mathf.Clamp (player.position.x, minX, maxX),Mathf.Clamp(player.position.y,minY,maxY), transform.position.z);
	}
}
