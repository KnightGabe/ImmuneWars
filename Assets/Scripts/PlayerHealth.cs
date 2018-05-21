using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public RectTransform HealthBar;
	public int MaxHP;
	public int CurrentHP;

	private void Start()
	{
		CurrentHP = MaxHP;
	}

	public void ChangeHealth(int health)
	{
		if (HealthBar != null)
		{
			HealthBar.sizeDelta = new Vector2(CurrentHP, HealthBar.sizeDelta.y);
		}
	}
	public void TakeDamage(int damage)
	{
		CurrentHP -= damage;
        ChangeHealth(CurrentHP);
        Debug.Log(damage);
		if (CurrentHP <= 0)
		{
			CurrentHP = 0;
			KillPlayer();
		}
	}
	public void KillPlayer()
	{
		//canInput = false;
		Debug.Log(gameObject.name + "Dead");
        var deadPlayer = GetComponent<PlayerSetup>();
        if (!deadPlayer.isDead)
        {
            StartCoroutine(deadPlayer.Respawn());
        }
    }
}
