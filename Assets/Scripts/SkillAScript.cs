using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAScript : MonoBehaviour {

	public GameObject Reference;
	string target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisonEnter(Collision col){
		if (col.gameObject.layer.ToString() == "PlayerRemoto") {
			target = col.gameObject.name;
			Destroy (this.gameObject);
		}	
	}
	void OnDestroy(){
		Reference.GetComponent<ShipManager> ().CmdEnemyPlayerHit (target, Reference.GetComponent<DummyShip> ().DamageSkillA);
	}
}
