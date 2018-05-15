using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryFire : MonoBehaviour {
	[SerializeField]
	[Header("Tiro")]
	protected int bulletDamage;

	public LayerMask enemyLayer;

	[SerializeField]
	protected float bulletRange;

	[SerializeField]
	protected float bulletCooldown;

	protected float bulletTimer = 0;

	public bool canFire = true;
	RayView laser;

	private void Start()
	{
		laser = GetComponent<RayView>();
	}

	private void Update()
	{
		CooldownTimer();
	}

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
		StartCoroutine(laser.ShotEffects());
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)), Camera.main.transform.forward, out hit, bulletDamage, enemyLayer))
		{
			EnemyPlayerHit(hit.collider.name, bulletDamage);
		}
		canFire = false;
		bulletTimer = 0;
	}

	public virtual void Comandos()
	{
		if (canFire)
		{
			HitscanShoot();
		}
	}

	public void EnemyPlayerHit(string target, int damage)
	{
		PlayerSetup player = GameManager.GetPlayer(target);
		player.health.TakeDamage(damage);
	}
}
