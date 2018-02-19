using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {

	Rigidbody rb;

	[SerializeField]
	float turnSpeed;
	[SerializeField]
	float forwardSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Turn();
		Move();
	}

	void Move()
	{
		float thrust = Input.GetAxis("Vertical");
		rb.velocity = (transform.forward * thrust).normalized * forwardSpeed;
	}

	void Turn()
	{
		float rotateY = Input.GetAxis("Mouse X");
		float rotateX = Input.GetAxis("Mouse Y");
		float rotateZ = Input.GetAxis("Horizontal");

		Vector3 rotation = new Vector3(rotateX, rotateY, rotateZ) * turnSpeed;

		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
	}
}
