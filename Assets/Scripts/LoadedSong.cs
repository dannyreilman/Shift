using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Loaded Song", fileName = "NewSong")]
public class LoadedSong : ScriptableObject
{
	static float WAIT_FOR_LOAD_SECONDS = 3.0f;
	static float WAIT_TIME = 0.01f;
	public float bpm;
	public float offset;
	public int beatsPerMeasure;
	public AudioClip song;
	public List<Note> notes;
	bool hasSong = false;
	//Called before playback (put preprocessing here)
	public IEnumerator PrepForPlaying()
	{
		notes.Sort((x, y) => x.timing.CompareTo(y.timing));
		float timePassed = 0;
		while(hasSong && song == null && timePassed < WAIT_FOR_LOAD_SECONDS)
		{
			yield return new WaitForSecondsRealtime(WAIT_TIME);
			timePassed += WAIT_TIME;
		}
	}

	public void SongIsLoading()
	{
		if(!hasSong)
		{
			hasSong = true;
			song = null;
			SongfileImporter.instance.importer.Loaded += SongFinishedLoading;
		}
	}

	public void SongFinishedLoading(AudioClip loadedClip)
	{
		if(song == null && hasSong)
		{
			song = loadedClip;
		}
		else if(hasSong)
		{
			hasSong = false;
			SongfileImporter.instance.importer.Loaded -= SongFinishedLoading;
			song = null;
		}
	}

	void OnDestroy()
	{
		if(hasSong)
			SongfileImporter.instance.importer.Loaded -= SongFinishedLoading;
	}
}
