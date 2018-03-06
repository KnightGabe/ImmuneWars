using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipManager : NetworkBehaviour {

	[Header("Objetos e Componentes")]
	public GameObject tiro;
	//public Transform tiroPos;

	Rigidbody rb; 

	[Header("Movimentacao")]
	[SerializeField]
	protected float turnSpeed, forwardSpeed, sideSpeed, verticalSpeed, baseSpeed;

	[SerializeField] [Range(0, 1)]
	float smoothRotation=0.125f;
	
	[Header("Inputs")]
	private float rotateY, rotateX, thrust, sideWays, vertical;

	[SerializeField][Header("Tiro")]
	protected int DamageBullet;

	[SerializeField][Header("Tiro")]
	protected float RangeBullet;

	[SerializeField][Header("Tiro")]
	protected float SpeedBullet;

	[SerializeField][Header("Tiro")]
	protected float CooldownBullet;

	protected float TimerBullet=0;

	[SerializeField][Header("Misc")]
	protected int MaxHP;
	[SerializeField]
	[Header("Misc")]
	protected LayerMask enemyLayer;
	[SerializeField]
	[Header("Misc")]
	protected LayerMask myLayer;
	protected float CurrentHP;
	public bool canInput = true;
	public bool canFire;
	private const string PlayerTag = "Player";
	// Use this for initialization
	protected virtual void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Turn();
		Move();
	}

	protected virtual void Update()
	{
		if (canInput)
		{
			GetInputs();
			Comandos();
		}
		CooldownTimer ();
	}		

	public void TakeDamage(int damage)
	{
		CurrentHP -= damage;
		if(CurrentHP <= 0)
		{
			//KillPlayer();
		}
	}

	void KillPlayer()
	{
		Network.Destroy(gameObject);
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
			if (hit.collider.tag == PlayerTag) {
				CmdEnemyPlayerHit (hit.collider.name,DamageBullet);
			}
			canFire = false;
			TimerBullet = 0;
		}
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
