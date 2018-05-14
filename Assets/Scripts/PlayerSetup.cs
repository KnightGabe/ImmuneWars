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
	private PlayerHealth health;

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
		IsDead = true;
		canInput = false;
		bxcol.enabled = false;
		Debug.Log(gameObject.name + "Dead");
		StartCoroutine(Respawn());
	}

	public IEnumerator Respawn()
	{
		yield return new WaitForSeconds(5f);
		health.ChangeHealth(MaxHP);
		transform.position = Origin;
		IsDead = false;
		canInput = true;
		bxcol.enabled = true;
	}
}
