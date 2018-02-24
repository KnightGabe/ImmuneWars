using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSetup :  NetworkBehaviour {

	[SerializeField]
	Behaviour[] disableObjects;
	
	Camera lobbyCamera;

	// Use this for initialization
	void Start () {
		if (!isLocalPlayer)
		{
			for (int i = 0; i < disableObjects.Length; i++)
			{
				disableObjects[i].enabled = false;
			}
		}
		else
		{
			lobbyCamera = Camera.main;
			if(lobbyCamera != null)
			{
				lobbyCamera.gameObject.SetActive(false);
			}
		}
	}

	private void OnDisable()
	{
		if(lobbyCamera != null)
		{
			lobbyCamera.gameObject.SetActive(true);
		}
	}
}
