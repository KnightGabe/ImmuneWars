using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	Transform player;

	[SerializeField] [Range(0, 1)]
	float smoothFollow;

	Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = player.position - transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 newPosition = player.position - (player.right * offset.x + player.up * offset.y + player.forward * offset.z);
		Vector3 smoothedMovement = Vector3.Lerp(transform.position, newPosition, smoothFollow);
		transform.position = smoothedMovement;
		transform.LookAt(player);
		Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, player.rotation, smoothFollow);
		transform.rotation = smoothedRotation;
		transform.rotation = player.rotation;
	}
}
