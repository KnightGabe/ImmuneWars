using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAScript : MonoBehaviour {

	public GameObject Reference;
	string target;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisonEnter(Collision col){
		if (col.gameObject.layer.ToString() == "PlayerRemoto") {
			target = col.gameObject.name;
			Destroy (this.gameObject);
		}	
	}

	/*public void AddSpeed (float speed){
		self.velocity = Reference.transform.forward * speed;
	}*/
	void OnDestroy(){
		Reference.GetComponent<ShipManager> ().CmdEnemyPlayerHit (target, Reference.GetComponent<DummyShip> ().DamageSkillA);
	}
}
