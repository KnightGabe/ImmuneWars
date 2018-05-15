using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	ShipMovement ship;
	PrimaryFire gun;
	SkillAScript skillA;

	// Use this for initialization
	void Start () {
		ship = GetComponent<ShipMovement>();
		gun = GetComponent<PrimaryFire>();
		skillA = GetComponent<SkillAScript>();
	}
	
	// Update is called once per frame
	void Update () {
		GetInputs();
	}

	void GetInputs()
	{
		ship.Thrust = Input.GetAxis("Vertical");
		ship.SideWays = Input.GetAxis("Horizontal");
		ship.RotateY = Input.GetAxis("Mouse X");
		ship.RotateX = Input.GetAxis("Mouse Y");
		ship.Vertical = Input.GetAxis("Jump");
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			gun.Comandos();
		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			skillA.ShootSkill();
		}
	}
}
