using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour {

	CursorLockMode gameMode;
	CursorMode visible;

	// Use this for initialization
	void Start () {
		//SetCursorState();
	}

	private void SetCursorState()
	{
		Cursor.lockState = gameMode;
		Cursor.visible = (CursorLockMode.Locked != gameMode);
	}
	
	void OnGUI()
	{
		GUILayout.BeginVertical();
		// Release cursor on escape keypress
		if (Input.GetKeyDown(KeyCode.Escape))
			Cursor.lockState = gameMode = CursorLockMode.None;

		switch (Cursor.lockState)
		{
			case CursorLockMode.None:
				GUILayout.Label("Cursor is normal");
				if (GUILayout.Button("Lock cursor"))
					gameMode = CursorLockMode.Locked;
				if (GUILayout.Button("Confine cursor"))
					gameMode = CursorLockMode.Confined;
				break;
			case CursorLockMode.Confined:
				GUILayout.Label("Cursor is confined");
				if (GUILayout.Button("Lock cursor"))
					gameMode = CursorLockMode.Locked;
				if (GUILayout.Button("Release cursor"))
					gameMode = CursorLockMode.None;
				break;
			case CursorLockMode.Locked:
				GUILayout.Label("Cursor is locked");
				if (GUILayout.Button("Unlock cursor"))
					gameMode = CursorLockMode.None;
				if (GUILayout.Button("Confine cursor"))
					gameMode = CursorLockMode.Confined;
				break;
		}

		GUILayout.EndVertical();

		SetCursorState();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
