using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public int damage;

	public float duration = 1f;

	// Use this for initialization
	void Start () {
		Invoke("SelfDestruct", duration);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.layer.Equals(gameObject.layer))
		{
			SelfDestruct();
		}
	}

	void SelfDestruct()
	{
		gameObject.SetActive(false);
	}
}
