using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLoader : MonoBehaviour, CursorUser
{
	public GameObject notePrefab;
	public RectTransform timingLine;
	private float timingHeight;
	public RectTransform spawnLine;
	private float spawnHeight;
	private float scrollRate = 200.0f;

	void Awake()
	{
		timingHeight = timingLine.position.y;
		spawnHeight = spawnLine.position.y;
	}

	public FlowManager.AcceptNote GetCursor()
	{
		return SpawnNote;
	}

    public float GetMillisecondDelay()
    {
		//Todo: replace with calculation for distance
		return - ((spawnHeight - timingHeight) / scrollRate) * 1000;
    }

    void SpawnNote(Note n)
	{
		Debug.Log("Spawning: " + n.lane);
		GameObject spawned = GameObject.Instantiate(notePrefab, new Vector3(0, spawnHeight, 0), Quaternion.identity, transform.GetChild(0));
		spawned.transform.localPosition = new Vector2(0, spawned.transform.localPosition.y);
		spawned.GetComponent<NoteMover>().SetSpeed(scrollRate);
	}


}
