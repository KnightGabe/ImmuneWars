using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public int damage;
	public GameObject emitter;

	public float duration = 1f;

	// Use this for initialization
	void Start () {
		Invoke("SelfDestruct", duration);
	}	

	void SelfDestruct()
	{
		gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider col)
	{
		if (!emitter.Equals(col.gameObject)&&(col.GetComponent<PlayerHealth>()!=null))
		{
			col.GetComponent<PlayerHealth>().TakeDamage(damage);
			gameObject.SetActive(false);
		}
	}
}
