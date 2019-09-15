using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Loaded Song", fileName = "NewSong")]
public class LoadedSong : ScriptableObject
{
	public float bpm;
	public float offset;
	public int beatsPerMeasure;
	public AudioClip song;
	public List<Note> notes;
	//Called before playback (put preprocessing here)
	public void PrepForPlaying()
	{
		Debug.Log("Called");
		notes.Sort((x, y) => x.timing.CompareTo(y.timing));
	}
}
