using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public Slider HealthBar;
	public int MaxHP;
	public int CurrentHP;
	private PlayerSetup respawn;

	private void Start()
	{
		CurrentHP = MaxHP;
		respawn = GetComponent<PlayerSetup>();
	}

	public void ChangeHealth(int health)
	{
		if (HealthBar != null)
		{
			HealthBar.value = health;
		}
	}
	public void TakeDamage(int damage)
	{
		CurrentHP -= damage;
		if (CurrentHP <= 0)
		{
			CurrentHP = 0;
			respawn.KillPlayer();
		}
	}
}
