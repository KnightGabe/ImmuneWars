using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootingScript : NetworkBehaviour {

	public GameObject tiro;

	public Transform tiroPos;

	public ShipMovement player;

	public float SpeedBullet;
	public float TimerBullet;
	public float RangeBullet;
	public float CooldownBullet;

	public int DamageBullet;

	public bool canFire;

	// Use this for initialization
	void Start () {
		player = GetComponentInChildren<ShipMovement>();
	}

	private void Update()
	{
		if (Input.GetMouseButton(0) && canFire)
		{
			CmdProjectileShoot();
		}
		TimerBullet += Time.deltaTime;
		if (TimerBullet >= CooldownBullet)
		{
			canFire = true;
		}
	}

	[Command]
	void CmdProjectileShoot()
	{
		if (isLocalPlayer)
		{
			GameObject nTiro = Instantiate(tiro, tiroPos.position, Quaternion.identity);
			NetworkServer.SpawnWithClientAuthority(nTiro, gameObject);
			RpcAddSpeed(nTiro);
		}
	}

	[ClientRpc]
	void RpcAddSpeed(GameObject tiro)
	{
		tiro.layer = LayerMask.NameToLayer("PlayerLocal");
		tiro.GetComponent<Rigidbody>().velocity = player.gameObject.transform.forward * SpeedBullet;
		canFire = false;
		TimerBullet = 0;
	}

	void HitscanShoot()
	{
		RaycastHit hit;
		Physics.Raycast(transform.position, transform.forward, out hit, RangeBullet, LayerMask.NameToLayer("PlayerLocal"));
		if (hit.collider.gameObject.layer == LayerMask.NameToLayer("PlayerRemoto"))
		{
			hit.collider.gameObject.GetComponent<ShipMovement>().TakeDamage(DamageBullet);
		}
		canFire = false;
		TimerBullet = 0;
	}
}
