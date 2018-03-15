using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ShipManager : NetworkBehaviour {

	[Header("Objetos e Componentes")]
	public GameObject tiro;
	//public Transform tiroPos;

	Rigidbody rb; 

	[Header("UI")]
	protected Slider HealthBar;
	[SerializeField]
	protected Canvas brianCanvas;

	[Header("Movimentacao")]
	[SerializeField]
	protected float turnSpeed;
	[SerializeField]
	protected float forwardSpeed, sideSpeed, verticalSpeed, baseSpeed;

	[SerializeField] [Range(0, 1)]
	float smoothRotation=0.125f;

	private float rotateY, rotateX, thrust, sideWays, vertical;

	[SerializeField][Header("Tiro")]
	protected int DamageBullet;


	[SerializeField]
	protected float RangeBullet;

	[SerializeField]
	protected float SpeedBullet;

	[SerializeField]
	protected float CooldownBullet;

	protected float TimerBullet=0;

	[SerializeField][Header("Misc")]
	protected int MaxHP;
	[SerializeField]
	[Header("Misc")]
	protected LayerMask enemyLayer;
	[SerializeField]
	protected LayerMask myLayer;
	[SyncVar]
	protected float CurrentHP;
	public bool canInput = true;
	public bool canFire = true;
	private const string PlayerTag = "Player";

	private MeshRenderer myRenderer;

	// Use this for initialization
	protected virtual void Start () {
		rb = GetComponent<Rigidbody>();
		InstantiateCanvas();
		myRenderer = GetComponent<MeshRenderer>();
		if (isLocalPlayer)
		{
			myRenderer.material.color = Color.blue;
		}
		else
		{
			myRenderer.material.color = Color.red;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!isLocalPlayer) {
			return;
		} else {
			SetHealth();
			Turn ();
			Move ();
		}
	}
	
	private void InstantiateCanvas()
	{
		Instantiate(brianCanvas);
		HealthBar = brianCanvas.GetComponentInChildren<Slider>();
		Debug.Log(HealthBar);
	}
	protected virtual void Update()
	{
		if (!isLocalPlayer) {
			return;
		} else {
			if (canInput) {
				GetInputs ();
				Comandos ();
			}
			CooldownTimer ();
		}
	}		

	private void SetHealth()
	{
		HealthBar.maxValue = MaxHP;
		HealthBar.value = CurrentHP;
	}

	public void TakeDamage(int damage)
	{
		if (isServer)
		{
			return;
		}
		CurrentHP -= damage;
		if(CurrentHP <= 0)
		{
			CurrentHP = 0;
			KillPlayer();
		}
	}

	void KillPlayer()
	{
		Debug.Log("Dead");
	}

	void GetInputs()
	{
		thrust = Input.GetAxis("Vertical");
		sideWays = Input.GetAxis("Horizontal");
		rotateY = Input.GetAxis("Mouse X");
		rotateX = Input.GetAxis("Mouse Y");
		vertical = Input.GetAxis("Jump");
	}

	void Move()
	{
		Vector3 movement = (((transform.forward * thrust) + (transform.up * vertical) - (transform.right * sideWays)).normalized);
		movement = new Vector3(movement.x * forwardSpeed, movement.y * verticalSpeed, (movement.z * forwardSpeed));
		rb.velocity = movement + (transform.forward * baseSpeed);
	}

	void Turn()
	{
		Vector3 newR = transform.rotation.eulerAngles;
		newR.x += rotateX *turnSpeed;
		newR.y += rotateY *turnSpeed;
		newR.z = 0;
		Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newR), smoothRotation);
		transform.rotation = smoothedRotation;
	}

	protected virtual void Comandos()
	{
		if (Input.GetMouseButton(0) && canFire)
		{
			HitscanShoot ();
		}
	}
	/*[Command]
	void CmdProjectileShoot(){
		GameObject nTiro = Instantiate (tiro,tiroPos.position,Quaternion.identity);
		nTiro.GetComponent<Rigidbody>().velocity=transform.forward*SpeedBullet;
		nTiro.layer=gameObject.layer;
		canFire=false;
		TimerBullet=0;
	}*/

	[Client]
	void HitscanShoot(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, RangeBullet,enemyLayer)){
			Debug.Log (hit.collider.name);
			if (hit.collider.tag == PlayerTag) {
				Debug.Log ("Entrou 1");
				CmdEnemyPlayerHit (hit.collider.name,DamageBullet);
				Debug.Log ("Entrou 2");
			}
		}
		canFire = false;
		TimerBullet = 0;
	}
	[Command]
	public void CmdEnemyPlayerHit(string target, int damage){
		ShipManager player = GameManager.GetPlayer (target);
		player.TakeDamage (damage);
	}
	protected virtual void CooldownTimer(){
		TimerBullet += Time.deltaTime;
		if (TimerBullet >= CooldownBullet)
		{
			canFire = true;
		}
	}
}
