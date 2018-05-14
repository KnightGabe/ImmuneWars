using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryFire : MonoBehaviour {
	[SerializeField]
	[Header("Tiro")]
	protected int bulletDamage;

	public GameObject tiro;

	public LayerMask enemyLayer;

	[SerializeField]
	protected float bulletRange;

	[SerializeField]
	protected float bulletSpeed;

	[SerializeField]
	protected float bulletCooldown;

	protected float bulletTimer = 0;

	public bool canFire = true;
	RayView laser;

	protected void CooldownTimer()
	{
		bulletTimer += Time.deltaTime;
		if (bulletTimer >= bulletCooldown)
		{
			canFire = true;
		}
	}

	void HitscanShoot()
	{
		Transform tiroPos = null;
		switch (laser.index)
		{
			case 1:
				tiroPos = laser.shotPosition1;
				laser.index++;
				break;
			case 2:
				tiroPos = laser.shotPosition2;
				laser.index--;
				break;
		}
		RaycastHit hit;
		Debug.DrawRay(transform.position, Camera.main.transform.forward);

		laser.myLine.SetPosition(0, tiroPos.position);
		StartCoroutine(laser.ShotEffects());
		if (Physics.Raycast(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)), Camera.main.transform.forward, out hit, bulletDamage, enemyLayer))
		{
			laser.myLine.SetPosition(1, hit.point);
			if (hit.collider != null)
			{
				EnemyPlayerHit(hit.collider.name, bulletDamage);
			}
		}
		else
		{
			laser.myLine.SetPosition(1, Camera.main.transform.position + (Camera.main.transform.forward * bulletRange));
		}
		canFire = false;
		bulletTimer = 0;
	}

	protected virtual void Comandos()
	{
		if (Input.GetMouseButton(0) && canFire)
		{
			HitscanShoot();
		}
	}

	public void EnemyPlayerHit(string target, int damage)
	{
		PlayerHealth player = GameManager.GetPlayer(target);
		player.TakeDamage(damage);
	}
}
