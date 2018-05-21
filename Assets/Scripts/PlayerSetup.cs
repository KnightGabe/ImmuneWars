using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviour {

    public Text scoreText;
    public Text respawnText;
	private Vector3 Origin;
	public PlayerHealth health;
	public bool canInput = true;
    public bool isDead = false;
    public int score=0;
    private float timer = 5;

	private void Start()
	{
        canInput = true;
		health = GetComponent<PlayerHealth>();
		Origin = transform.position;
	}

    private void Update()
    {
        scoreText.text = "Score :" + score.ToString();
        if (isDead)
        {
            respawnText.text = "You Died \n" + Mathf.RoundToInt(timer -= Time.deltaTime).ToString();
        }
        else
        {
            timer = 5;
        }
    }

    public IEnumerator Respawn()
	{
        canInput = false;
		yield return new WaitForSeconds(5f);
        respawnText.text = "";
		health.ChangeHealth(health.MaxHP);
		transform.position = Origin;
		isDead = false;
		canInput = true;
	}
}
