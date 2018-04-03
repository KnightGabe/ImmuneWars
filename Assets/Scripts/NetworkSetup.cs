using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(ShipManager))]
public class NetworkSetup :  NetworkBehaviour {

	[SerializeField]
	Behaviour[] disableObjects;

	[SerializeField]
	string RemotePlayerLayer = "PlayerRemoto";
	
	Camera lobbyCamera;

	ShipManager player;

	//CursorLockMode currentMode;

	// Use this for initialization
	void Start () {
		player = GetComponent<ShipManager>();
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
		ShipManager Player = GetComponent<ShipManager> ();
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
