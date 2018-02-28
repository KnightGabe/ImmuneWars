using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static Dictionary<string, ShipMovement> players = new Dictionary<string, ShipMovement>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public static void RegisterPlayer(string NetID, ShipMovement player){
		string PlayerID = "Player " + NetID;
		players.Add (PlayerID, player);
		player.transform.name = PlayerID;

	}
	public static void RemovePlayer(string PlayerID){
		players.Remove (PlayerID);
	}
	public static ShipMovement GetPlayer(string PlayerID){
		return players [PlayerID];
	}
}
