using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAScript : MonoBehaviour {

	PlayerSetup reference;
	[SerializeField]
	protected float bulletSpeed;
	[SerializeField]
	private float cooldownTime;
	public bool canShoot;

	public GameObject missile;

	private float timer;

	private void Start()
	{
		reference = GetComponent<PlayerSetup>();
		timer = cooldownTime;
	}

	public void ShootSkill()
	{
		if (canShoot)
		{
			GameObject missileClone = Instantiate(missile, transform.position, Quaternion.identity);
			missileClone.GetComponent<BulletScript>().enemyLayer = reference.enemyLayer;
			missileClone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
		}
		canShoot = false;
	}

	private void Update()
	{
		if (!canShoot)
		{
			timer -= Time.deltaTime;
			if(timer <= 0)
			{
				ResetCooldown();
			}
		}
	}

	private void ResetCooldown()
	{
		canShoot = true;
		timer = cooldownTime;
	}
}
