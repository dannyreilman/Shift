using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLoader : MonoBehaviour, CursorUser
{
	public GameObject notePrefab;
	public static float BASE_DELAY = 3.0f;

	public FlowManager.AcceptNote GetCursor()
	{
		return SpawnNote;
	}

    public float GetMillisecondDelay()
    {
		return BASE_DELAY * -1000;
    }

    void SpawnNote(Note n)
	{
		//Debug.Log("Spawning: " + n.lane);
		GameObject spawned = GameObject.Instantiate(notePrefab, Vector3.zero, Quaternion.identity, transform.GetChild(n.lane));
		spawned.GetComponent<NoteMover>().lane = n.lane;
		n.rendered = spawned.GetComponent<RenderedNote>();
		n.rendered.InitializeNote(n);
	}


}
