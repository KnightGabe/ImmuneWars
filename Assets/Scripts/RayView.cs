using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayView : MonoBehaviour {

	public LineRenderer myLine;
	public AudioSource shotSound;
	public Transform shotPosition1, shotPosition2;
	public int index = 1;

	// Use this for initialization
	void Start () {
		myLine = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public IEnumerator ShotEffects()
	{
		//shotSound.Play();
		myLine.enabled = true;
		yield return new WaitForSeconds(0.02f);
		myLine.enabled = false;
		
	}
	
}
