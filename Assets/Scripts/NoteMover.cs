using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMover : MonoBehaviour {

	float speed;
	public void SetSpeed(float speed)
	{
		this.speed = speed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
	}
}
