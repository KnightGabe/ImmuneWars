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

		CurrentHP = MaxHP;
		base.Start ();
	}
	protected override void Update(){
		base.Update ();
	}
	protected override void Comandos ()
	{
		base.Comandos ();
		if (Input.GetKeyDown (KeyCode.F) && !OnCDSkillA) {
			SkillA ();
		}
	}

	void SkillA(){
		GameObject nTiro = Instantiate (tiro,transform.position,Quaternion.identity);
		nTiro.GetComponent<Rigidbody>().velocity=transform.forward*SpeedBullet;
		nTiro.GetComponent<SkillAScript>().Reference = this;
		//nTiro.GetComponent<SkillAScript> ().AddSpeed (SpeedBullet);

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
}
