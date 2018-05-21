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

    private Camera myCam;

	public bool canFire = true;
	RayView laser;

	private void Start()
	{
		laser = GetComponent<RayView>();
        myCam = GetComponentInChildren<Camera>();
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
		if (Physics.Raycast(myCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)), myCam.transform.forward, out hit, bulletRange))
		{
			if ((!hit.collider.gameObject.Equals (gameObject)) && (hit.collider.GetComponent<PlayerHealth> () != null)) {
				hit.collider.GetComponent<PlayerHealth> ().TakeDamage (bulletDamage);
                if (hit.collider.GetComponent<PlayerHealth>().CurrentHP <= 0)
                {
                    var enemyKill = hit.collider.GetComponent<PlayerSetup>();
                    if (!enemyKill.isDead)
                    {
                        GetComponent<PlayerSetup>().score++;
                        enemyKill.isDead = true;
                    }
                }
			}
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
}
