using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {
	
	Rigidbody rb; 
	[Header("Movimentacao")]
	[SerializeField]
	protected float turnSpeed;
	[SerializeField]
	protected float forwardSpeed, sideSpeed, verticalSpeed, baseSpeed;

	[SerializeField] [Range(0, 1)]
	float smoothRotation=0.125f;

	private float rotateY, rotateX, thrust, sideWays, vertical;

	public float RotateY
	{
		get
		{
			return rotateY;
		}

		set
		{
			rotateY = value;
		}
	}

	public float RotateX
	{
		get
		{
			return rotateX;
		}

		set
		{
			rotateX = value;
		}
	}

	public float Thrust
	{
		get
		{
			return thrust;
		}

		set
		{
			thrust = value;
		}
	}

	public float SideWays
	{
		get
		{
			return sideWays;
		}

		set
		{
			sideWays = value;
		}
	}

	public float Vertical
	{
		get
		{
			return vertical;
		}

		set
		{
			vertical = value;
		}
	}


	// Use this for initialization
	protected void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Turn();
		Move();	
	}

	void Move()
	{
		Vector3 movement = (((transform.forward * Thrust) + (transform.up * Vertical) - (transform.right * SideWays)).normalized);
		movement = new Vector3(movement.x * forwardSpeed, movement.y * verticalSpeed, (movement.z * forwardSpeed));
		rb.velocity = movement + (transform.forward * baseSpeed);
	}

	void Turn()
	{
		Vector3 newR = transform.rotation.eulerAngles;
		newR.x += RotateX * turnSpeed;
		newR.y += RotateY * turnSpeed;
		newR.z = 0;
		Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newR), smoothRotation);
		transform.rotation = smoothedRotation;
	}	
}
