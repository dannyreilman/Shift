using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMover : MonoBehaviour
{
	[HideInInspector()]
	public int lane;
	float progress = 0.0f;

	NoteLoader following;

	void Start()
	{
		PauseManager.PausableUpdate += PausableUpdate;
	}

	void OnDestroy()
	{
		PauseManager.PausableUpdate -= PausableUpdate;
	}
	
	// Update is called once per frame
	void PausableUpdate () 
	{
		progress += Time.deltaTime / NoteLoader.BASE_DELAY;

		transform.position = new Vector3(0, LaneManager.instance.GetHeight(progress), 0);
		transform.localPosition = new Vector2(0, transform.localPosition.y);
	}
}
