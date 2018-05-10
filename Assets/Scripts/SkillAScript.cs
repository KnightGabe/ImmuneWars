using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SkillAScript : NetworkBehaviour {

	public ShipManager Reference;
	string target;
	
	void OnTriggerEnter(Collider col)
	{
		if (Reference.enemyLayer == (Reference.enemyLayer | ( 1 << col.gameObject.layer)))
		{
			target = col.gameObject.name;
			Reference.EnemyPlayerHit(target, Reference.GetComponent<DummyShip>().DamageSkillA);
			gameObject.SetActive(false);
		}	
	}
}
