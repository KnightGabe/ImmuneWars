using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DummyShip : ShipManager {

	// Use this for initialization
	private float CooldownSkillA = 10f;
	private float TimerSkillA=0f;
	private bool OnCDSkillA;
	private float RangeSkillA=100f;
	public int DamageSkillA=50;

	protected override void Start () {
		
		base.Start ();
		MaxHP = 100;
		CurrentHP = MaxHP;
		turnSpeed = 10f;
		forwardSpeed = 5f;
		sideSpeed = 2f;
		verticalSpeed = 3f;
		baseSpeed=0f;
		DamageBullet = 10;
		RangeBullet = 100f;
		SpeedBullet = 100f;
		CooldownBullet = 0.02f;
	}
	protected override void Comandos ()
	{
		base.Comandos ();
		if (Input.GetKeyDown (KeyCode.F) && !OnCDSkillA) {
			CmdSkillA ();
		}
	}
	[Command]
	void CmdSkillA(){
		GameObject nTiro = Instantiate (tiro,transform.position,Quaternion.identity);
		nTiro.GetComponent<Rigidbody>().velocity=transform.forward*SpeedBullet;
		nTiro.GetComponent<SkillAScript> ().Reference = this.gameObject;
		NetworkServer.Spawn (nTiro);
		OnCDSkillA = true;
	}
	protected override void CooldownTimer(){
		base.CooldownTimer ();
		TimerSkillA += Time.deltaTime;
		if (TimerSkillA >= CooldownSkillA){
			OnCDSkillA = false;
		}
	}
}
