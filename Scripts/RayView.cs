using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayView : MonoBehaviour {

	private LineRenderer myLine;
	private AudioSource shotSound;
	public Transform shotPosition1, shotPosition2;
	Transform tiroPos;
	public int index = 1;
    private Camera myCam;

	// Use this for initialization
	void Start () {
		myLine = GetComponent<LineRenderer>();
        myCam = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public IEnumerator ShotEffects()
	{
		//shotSound.Play();
		SwitchLaser();
		SetLaserPositions();
		myLine.enabled = true;
		yield return new WaitForSeconds(0.02f);
		myLine.enabled = false;
	}

	private void SetLaserPositions()
	{
		myLine.SetPosition(0, tiroPos.position);
		RaycastHit hit;
		if (Physics.Raycast(myCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)), myCam.transform.forward, out hit))
			myLine.SetPosition(1, hit.point);
	}

	void SwitchLaser()
	{
		switch (index)
		{
			case 1:
				tiroPos = shotPosition1;
				index++;
				break;
			case 2:
				tiroPos = shotPosition2;
				index--;
				break;
		}
	}
	
}
