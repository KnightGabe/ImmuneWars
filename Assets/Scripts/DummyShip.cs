using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DummyShip : ShipManager {

	// Use this for initialization
	private float CooldownSkillA = 2f;
	private float TimerSkillA=0f;
	private bool OnCDSkillA;
	private float RangeSkillA=100f;
	public int DamageSkillA=50;

	protected override void Start () {

		MontarPadrao ();
		base.Start ();
	}
	protected override void Update(){
		base.Update ();
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
		//nTiro.GetComponent<SkillAScript> ().AddSpeed (SpeedBullet);
		NetworkServer.Spawn (nTiro);

		OnCDSkillA = true;
		TimerSkillA = 0f;
	}
	protected override void CooldownTimer(){
		base.CooldownTimer ();
		TimerSkillA += Time.deltaTime;
		if (TimerSkillA >= CooldownSkillA){
			OnCDSkillA = false;
		}
	}
	protected void MontarPadrao(){
		turnSpeed = 5f;
		forwardSpeed = 5f;
		sideSpeed = 5f;
		verticalSpeed = 5f;
		baseSpeed = 5f;
		DamageBullet = 5;
		RangeBullet = 100f;
		SpeedBullet = 30f;
		CooldownBullet = 0.3f;
		CurrentHP = MaxHP;
	}
}
