using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour {
	[SerializeField]
	private Texture2D cursorTexture;
	private Vector2 hotSpot = Vector2.zero;
	private CursorMode cursorMode = CursorMode.Auto;

	void OnMouseEnter() {
		Debug.Log ("Passou o Mouse");
		Cursor.SetCursor (cursorTexture, hotSpot, cursorMode);
	}

	void OnMouseExit() {
		Cursor.SetCursor (null, Vector2.zero, cursorMode);
		Debug.Log ("Tirou o Mouse");
	}
}
