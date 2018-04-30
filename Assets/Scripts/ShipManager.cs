using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ShipManager : NetworkBehaviour {

	[Header("Objetos e Componentes")]
	public GameObject tiro;
	BoxCollider bxcol;
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
	public LayerMask enemyLayer;
	[SerializeField]
	public LayerMask myLayer;
	[SyncVar(hook = "ChangeHealth")]
	public int CurrentHP;
	public bool canInput = true;
	public bool canFire = true;
	[SyncVar]
	private bool IsDead=false;
	public bool isRlyDead{
		get{ return IsDead; }
		protected set{ IsDead = value; }
	}
	private const string PlayerTag = "Player";
	private Vector3 Origin;

	private MeshRenderer myRenderer;

	RayView laser;
	

	// Use this for initialization
	protected virtual void Start () {
		bxcol = GetComponent<BoxCollider>();
		rb = GetComponent<Rigidbody>();
		myRenderer = GetComponentInChildren<MeshRenderer>();
		laser = GetComponentInChildren<RayView>();
		Origin = transform.position;
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
	void FixedUpdate ()
	{
		if (!isLocalPlayer || !canInput)
		{
			return;
		}
		Turn();
		Move();	
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
	[ClientRpc]
	public void RpcTakeDamage(int damage)
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

	private void KillPlayer()
	{
		IsDead = true;
		canInput = false;
		bxcol.enabled = false;
		Debug.Log(gameObject.name + "Dead");
		StartCoroutine (Respawn ());
	}

	IEnumerator Respawn(){
		yield return new WaitForSeconds (5f);
		ChangeHealth(MaxHP);
		transform.position = Origin;
		IsDead = false;
		canInput = true;
		bxcol.enabled = true;

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
	 
	[Client]
	void HitscanShoot()
	{
		Transform tiroPos = null;
		switch (laser.index)
		{
			case 1:
				tiroPos = laser.shotPosition1;
				laser.index++;
				break;
			case 2:
				tiroPos = laser.shotPosition2;
				laser.index--;
				break;
		}
		RaycastHit hit;
		Debug.DrawRay(transform.position, Camera.main.transform.forward);

		laser.myLine.SetPosition(0, tiroPos.position);
		StartCoroutine(laser.ShotEffects());
		if (Physics.Raycast (Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)), Camera.main.transform.forward , out hit, RangeBullet, enemyLayer))
		{
			laser.myLine.SetPosition(1, hit.point);
		if (hit.collider != null) {
				CmdEnemyPlayerHit (hit.collider.name,DamageBullet);
			}
		}
		else
		{
			laser.myLine.SetPosition(1, Camera.main.transform.position + (Camera.main.transform.forward * RangeBullet));
		}
		canFire = false;
		TimerBullet = 0;
	}
	[Command]
	public void CmdEnemyPlayerHit(string target, int damage){
		ShipManager player = GameManager.GetPlayer (target);
		player.RpcTakeDamage (damage);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == enemyLayer)
		{
			BulletScript enemyBullet = other.GetComponent<BulletScript>();
			if (enemyBullet != null)
			{
				RpcTakeDamage(enemyBullet.damage);
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
