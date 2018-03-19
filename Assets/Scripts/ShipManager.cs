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
	[SyncVar(hook = "ChangeHealth")]
	public int CurrentHP;
	public bool canInput = true;
	public bool canFire = true;
	private const string PlayerTag = "Player";

	private MeshRenderer myRenderer;

	RayView laser;
	

	// Use this for initialization
	protected virtual void Start () {
		rb = GetComponent<Rigidbody>();
		myRenderer = GetComponent<MeshRenderer>();
		laser = GetComponentInChildren<RayView>();
		if (isLocalPlayer)
		{
			myRenderer.material.color = Color.blue;
			InstantiateCanvas();

			HealthBar.maxValue = MaxHP;
			HealthBar.value = MaxHP;
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
			if (canInput)
			{
				Turn();
				Move();
			}
		}
	}

	void ChangeHealth(int health)
	{
		if (HealthBar!= null)
		{
			HealthBar.value = health;
		}
	}

	private void InstantiateCanvas()
	{
		Canvas newCanvas = Instantiate(brianCanvas);
		HealthBar = newCanvas.GetComponentInChildren<Slider>();
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
	public void TakeDamage(int damage)
	{
		if (isServer)
		{
			CurrentHP -= damage;
			if (CurrentHP <= 0)
			{
				CurrentHP = 0;
				KillPlayer();
			}
		}
	}

	void KillPlayer()
	{
		canInput = false;
		Debug.Log(gameObject.name + "Dead");
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
		Debug.DrawRay(transform.position, Camera.main.transform.forward);
		//StartCoroutine(laser.DrawLine(Camera.main.transform.forward * RangeBullet));
		if (Physics.Raycast (transform.position, Camera.main.transform.forward, out hit, RangeBullet,enemyLayer)){
			Debug.Log (hit.collider.name);
			if (hit.collider != null) {
				Debug.Log ("Entrou 1");
				CmdEnemyPlayerHit (hit.collider.name,DamageBullet);
				//hit.collider.GetComponent<DummyShip>().TakeDamage(DamageBullet);
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

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == enemyLayer)
		{
			BulletScript enemyBullet = other.GetComponent<BulletScript>();
			if (enemyBullet != null)
			{
				TakeDamage(enemyBullet.damage);
			}
		}
	}
	protected virtual void CooldownTimer(){
		TimerBullet += Time.deltaTime;
		if (TimerBullet >= CooldownBullet)
		{
			canFire = true;
		}
	}
}
