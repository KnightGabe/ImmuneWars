using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	private ShipManager player;

	private string thrustAxis;
	private string sidewaysAxis;
	private string rotateYAxis;
	private string rotateXAxis;
	private string verticalAxis;
<<<<<<< HEAD
    private string negThrustAxis;

    private int ID;
=======

	private int ID;
>>>>>>> 51b20b77742a5b9844a1a4f2ec8ed25d557488e7

	// Use this for initialization
	void Start() {
		SetPlayerID(1);
		player = GetComponent<ShipManager>();
	}

	// Update is called once per frame
	void Update() {
		PlayerInputs();
	}
	
	public void SetPlayerID(int ID)
	{
<<<<<<< HEAD
		verticalAxis = "Vertical" + ID.ToString();
		sidewaysAxis = "Horizontal" + ID.ToString();
		rotateYAxis = "HorizontalR" + ID.ToString();
		rotateXAxis = "VerticalR" + ID.ToString();
		thrustAxis = "Thrust" + ID.ToString();
        negThrustAxis = "-Thrust" + ID.ToString();

        this.ID = ID;
=======
		thrustAxis = "Vertical" + ID.ToString();
		sidewaysAxis = "Horizontal" + ID.ToString();
		rotateYAxis = "HorizontalR" + ID.ToString();
		rotateXAxis = "VerticalR" + ID.ToString();
		verticalAxis = "Jump" + ID.ToString();
		this.ID = ID;
>>>>>>> 51b20b77742a5b9844a1a4f2ec8ed25d557488e7
	}

	void PlayerInputs()
	{
<<<<<<< HEAD
		player.Movement.Thrust = Input.GetAxis(thrustAxis) - Input.GetAxis(negThrustAxis);
=======
		player.Movement.Thrust = Input.GetAxis(thrustAxis);
>>>>>>> 51b20b77742a5b9844a1a4f2ec8ed25d557488e7
		player.Movement.SideWays = Input.GetAxis(sidewaysAxis);
		player.Movement.RotateY = Input.GetAxis(rotateYAxis);
		player.Movement.RotateX = Input.GetAxis(rotateXAxis);
		player.Movement.Vertical = Input.GetAxis(verticalAxis);
<<<<<<< HEAD


        switch (ID)
=======
		
		switch (ID)
>>>>>>> 51b20b77742a5b9844a1a4f2ec8ed25d557488e7
		{
			case 1:
				if (Input.GetKeyDown(KeyCode.Joystick1Button5))
				{
					player.Fire.Comandos();
				}
				if (Input.GetKeyDown(KeyCode.Joystick1Button4))
				{
					player.SkillA.ShootSkill();
				}
				break;
			case 2:
				if (Input.GetKeyDown(KeyCode.Joystick2Button5))
				{
					player.Fire.Comandos();
				}
				if (Input.GetKeyDown(KeyCode.Joystick2Button4))
				{
					player.SkillA.ShootSkill();
				}
				break;
			case 3:
				if (Input.GetKeyDown(KeyCode.Joystick3Button5))
				{
					player.Fire.Comandos();
				}
				if (Input.GetKeyDown(KeyCode.Joystick3Button4))
				{
					player.SkillA.ShootSkill();
				}
				break;
			case 4:
				if (Input.GetKeyDown(KeyCode.Joystick4Button5))
				{
					player.Fire.Comandos();
				}
				if (Input.GetKeyDown(KeyCode.Joystick4Button4))
				{
					player.SkillA.ShootSkill();
				}
				break;
		}
		
	}
}
