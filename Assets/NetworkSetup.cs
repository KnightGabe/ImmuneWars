using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSetup :  NetworkBehaviour {

	[SerializeField]
	Behaviour[] disableObjects;

	[SerializeField]
	string RemotePlayerLayer = "PlayerRemoto";
	
	Camera lobbyCamera;

	ShipMovement player;
	//CursorLockMode currentMode;

	// Use this for initialization
	void Start () {
		player = GetComponent<ShipMovement>();
		if (!isLocalPlayer)
		{
			DisableComponents ();
			SetRemotePlayer ();
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

	public override void OnStartClient(){
		base.OnStartClient ();
		string NetID = GetComponent<NetworkIdentity> ().netId.ToString();
		ShipMovement Player = GetComponent<ShipMovement> ();
		GameManager.RegisterPlayer (NetID, Player);
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
		GameManager.RemovePlayer (transform.name);
	}
	void DisableComponents(){
		player.gameObject.layer = LayerMask.NameToLayer("PlayerRemoto");
		for (int i = 0; i < disableObjects.Length; i++)
		{
			disableObjects[i].enabled = false;
		}
	}
	void SetRemotePlayer (){
		gameObject.layer = LayerMask.NameToLayer (RemotePlayerLayer);
	}
}
