using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour {

	protected Canvas brianCanvas;
	public LayerMask enemyLayer;
	[SerializeField]
	public LayerMask myLayer;
	private Vector3 Origin;
	private MeshRenderer myRenderer;
	public PlayerHealth health;
	public bool isDead;
	public bool canInput;

	private void Start()
	{
		health = GetComponent<PlayerHealth>();
		Origin = transform.position;
	}

	private void InstantiateCanvas()
	{
		Canvas newCanvas = Instantiate(brianCanvas);
		myRenderer.material.color = Color.blue;
	}

	public void KillPlayer()
	{
		isDead = true;
		canInput = false;
		Debug.Log(gameObject.name + "Dead");
		StartCoroutine(Respawn());
	}

	public IEnumerator Respawn()
	{
		yield return new WaitForSeconds(5f);
		health.ChangeHealth(health.MaxHP);
		transform.position = Origin;
		isDead = false;
		canInput = true;
	}
}
