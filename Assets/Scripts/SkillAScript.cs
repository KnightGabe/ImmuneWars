﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAScript : MonoBehaviour {

	PlayerSetup reference;
	[SerializeField]
	protected float bulletSpeed;
	[SerializeField]
	private float cooldownTime;
	public bool canShoot = true;

    private Camera myCam;

	public GameObject missile;

	private float timer;

	private void Start()
	{
		reference = GetComponent<PlayerSetup>();
		timer = cooldownTime;
        myCam = GetComponentInChildren<Camera>();
	}

	public void ShootSkill()
	{
		if (canShoot)
        {
			GameObject missileClone = Instantiate(missile, transform.position, Quaternion.identity);
			missileClone.GetComponent<BulletScript>().emitter = gameObject;
			missileClone.GetComponent<Rigidbody>().velocity = myCam.transform.forward * bulletSpeed;
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
