﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipMovement : NetworkBehaviour {

	[Header("Objetos e Componentes")]
	public GameObject tiro;
	public Transform tiroPos;
	LayerMask self;

	Rigidbody rb; 

	[Header("Movimentacao")]
	[SerializeField]
	protected float turnSpeed, forwardSpeed, sideSpeed, verticalSpeed, baseSpeed;

	[SerializeField] [Range(0, 1)]
	float smoothRotation;
	
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
	protected float MaxHP;
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
	void Start () {
		rb = GetComponent<Rigidbody>();
		CurrentHP = MaxHP;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Turn();
		Move();
	}

	private void Update()
	{
		if (canInput)
		{
			GetInputs();
			Commandos();
		}
		TimerBullet += Time.deltaTime;
		if (TimerBullet >= CooldownBullet)
		{
			canFire = true;
		}
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

	void Commandos()
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
		if (Physics.Raycast (transform.position, transform.forward, out hit, RangeBullet,self)){
			if (hit.collider.tag == PlayerTag) {
				CmdEnemyPlayerHit (hit.collider.name);
			}
			//ShipMovement target = hit.transform.GetComponent<ShipMovement> ();
			//if (target != null) {
				//target.TakeDamage (DamageBullet);
			//}
			canFire = false;
			TimerBullet = 0;
		}
	}
	[Command]
	void CmdEnemyPlayerHit(string target){
		ShipMovement player = GameManager.GetPlayer (target);
		player.TakeDamage (DamageBullet);
	}
}
