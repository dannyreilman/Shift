using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMover : MonoBehaviour
{
	[HideInInspector()]
	public int lane;
	float progress = 0.0f;

	NoteLoader following;
	
	// Update is called once per frame
	void Update () 
	{
		progress += Time.deltaTime / NoteLoader.BASE_DELAY;

		transform.position = new Vector3(0, LaneManager.instance.GetHeight(progress), 0);
		transform.localPosition = new Vector2(0, transform.localPosition.y);
	}
}
