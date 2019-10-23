using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;


//Handles all control flow and song cursors
public class FlowManager : MonoBehaviour {
	public static FlowManager instance = null;
	public static Timestamp currentTime
	{
		get
		{
			return instance.time;
		}
	}
    public string abortScene;

	private SongPlayer player;
	private NoteLoader loader;
	private TimingManager timing;
	private TimeKeeper timekeeper;

	public LoadedSong song;
	private int songNoteIndex = 0;
	
	public Timestamp time = new Timestamp();
	private Dictionary<float, System.Action<Note>> loadCursors = new Dictionary<float, System.Action<Note>>();
	//Total offset to make negative cursors 0
	public float totalOffset = 0;
	//The perceived offset in the music
	public float actualOffset;

	public float scoreDelay;
	public string scoreScene;
	private bool waiting = true;

	void Awake()
	{
		if(instance == null || instance.Equals(null))
		{
			instance = this;
			player = GetComponentInChildren<SongPlayer>();
			loader = GetComponentInChildren<NoteLoader>();
			timing = GetComponentInChildren<TimingManager>();
			timekeeper = GetComponentInChildren<TimeKeeper>();
		}
		else
		{
			Destroy(this);
		}
	}

	void Abort()
	{
		Debug.Log("Song didn't load properly, aborting");
		MapLoader.instance.ClearLoaded();
		SceneManager.LoadScene(abortScene, LoadSceneMode.Single);
	}

	// Use this for initialization
	void Start ()
	{
		try
		{
			if(MapLoader.instance.loaded == null)
			{
				MapLoader.instance.LoadMap(0, 0);
			}
			LoadedSong initSong = MapLoader.instance.loaded;

			if(initSong != null)
			{
				timekeeper.SetBPM(initSong.bpm);
				StartCoroutine(PlaySong(initSong));
			}

			PauseManager.PausableUpdate += PausableUpdate;
		}
		catch(ArgumentException e)
		{
			Abort();
			Debug.Log(e);
		}

		ScoreManager.instance.ResetScore();
	}

	void OnDestroy()
	{
		PauseManager.PausableUpdate -= PausableUpdate;
	}

	void AddCursor(CursorUser user)
	{
		if(user.GetMillisecondDelay() < -1 * totalOffset)
		{
			totalOffset = -1 * user.GetMillisecondDelay();
		}
		loadCursors.Add(user.GetMillisecondDelay(), delegate {});
		loadCursors[user.GetMillisecondDelay()] += user.GetCursor();
	}

	IEnumerator StartAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		waiting = false;
	}

	IEnumerator PlaySong(LoadedSong song)
	{
		//This will wait for songfile to load, if possible
		yield return song.PrepForPlaying();
		if(song.song == null)
		{
			Abort();
			yield break;
		}

		this.song = song;
		AddCursor(loader);
		AddCursor(timing);
		actualOffset = -1 * (totalOffset + song.offset);
		if(actualOffset > 0)
		{
			player.Play(song, 0);
			StartCoroutine(StartAfterDelay(actualOffset / 1000.0f));
		}
		else
		{
			player.Play(song, (-1 * actualOffset)/1000.0f);
			StartCoroutine(StartAfterDelay(0));
		}
	}

	IEnumerator TriggerAfterDelay(float delay, System.Action<Note> toTrigger, Note n)
	{
		yield return new WaitForSeconds(delay);
		toTrigger(n);
	}

	IEnumerator LoadScoreScreenAfterDelay()
	{
		yield return new WaitForSeconds(totalOffset/1000.0f);

		StartCoroutine(player.FadeOut(scoreDelay));
		yield return new WaitForSeconds(scoreDelay);

		SceneManager.LoadScene(scoreScene);
	}

	void PausableUpdate()
	{
		if(!waiting)
		{
			time.addTime(song.beatsPerMeasure, Time.deltaTime, song.bpm);
			while(songNoteIndex < song.notes.Count &&
				time > song.notes[songNoteIndex].timing)
			{
				foreach(KeyValuePair<float, System.Action<Note>> entry in loadCursors)
				{
					StartCoroutine(TriggerAfterDelay((totalOffset + entry.Key) / 1000.0f, 
													entry.Value, 
													song.notes[songNoteIndex]));
				}
				++songNoteIndex;
			}

			if(songNoteIndex == song.notes.Count)
			{
				//Song ended, trigger exit fade
				StartCoroutine(LoadScoreScreenAfterDelay());
				++songNoteIndex;
			}
		}
	}
}
