using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayView : MonoBehaviour {

	LineRenderer myLine;

	// Use this for initialization
	void Start () {
		myLine = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public IEnumerator DrawLine(Vector3 destination)
	{
		myLine.enabled = true;
		myLine.SetPosition(0, transform.position);
		myLine.SetPosition(1, destination);
		yield return new WaitForSeconds(0.1f);
		myLine.enabled = false;
	}
	
}
