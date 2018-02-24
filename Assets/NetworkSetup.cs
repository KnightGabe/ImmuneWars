using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSetup :  NetworkBehaviour {

	[SerializeField]
	Behaviour[] disableObjects;
	
	Camera lobbyCamera;

	ShipMovement player;
	//CursorLockMode currentMode;

	// Use this for initialization
	void Start () {
		player = GetComponentInChildren<ShipMovement>();
		if (!isLocalPlayer)
		{
			player.gameObject.layer = LayerMask.NameToLayer("PlayerRemoto");
			for (int i = 0; i < disableObjects.Length; i++)
			{
				disableObjects[i].enabled = false;
			}
		}
		else
		{
			player.gameObject.layer = LayerMask.NameToLayer("PlayerLocal");
			//currentMode = CursorLockMode.Locked;
			lobbyCamera = Camera.main;
			if(lobbyCamera != null)
			{
				lobbyCamera.gameObject.SetActive(false);
			}
		}
	}

	private void Update()
	{
		//Cursor.lockState = currentMode;
	}

	private void OnDisable()
	{
		if (lobbyCamera != null)
		{
			lobbyCamera.gameObject.SetActive(true);
			//currentMode = CursorLockMode.None;
		}
	}
}
