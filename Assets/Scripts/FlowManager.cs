using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

	private SongPlayer player;
	private NoteLoader loader;
	private TimingManager timing;
	public LoadedSong initSong;
	private LoadedSong song;
	private int songNoteIndex = 0;
	
	private Timestamp time = new Timestamp();
	
	public delegate void AcceptNote(Note n);
	private Dictionary<float, AcceptNote> loadCursors = new Dictionary<float, AcceptNote>();
	//Total offset to make negative cursors 0
	private float totalOffset = 0;
	private bool waiting = true;
	void Awake()
	{
		if(instance == null || instance.Equals(null))
		{
			instance = this;
			player = GetComponentInChildren<SongPlayer>();
			loader = GetComponentInChildren<NoteLoader>();
			timing = GetComponentInChildren<TimingManager>();
		}
		else
		{
			Destroy(this);
		}
	}

	// Use this for initialization
	void Start ()
	{
		PlaySong(initSong);
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
	void PlaySong(LoadedSong song)
	{
		this.song = song;
		AddCursor(loader);
		AddCursor(timing);
		float adjustedOffset = -1 * (totalOffset + song.offset);
		if(adjustedOffset > 0)
		{
			player.Play(song, 0);
			StartCoroutine(StartAfterDelay(adjustedOffset / 1000.0f));
		}
		else
		{
			player.Play(song, (-1 * adjustedOffset)/1000.0f);
			StartCoroutine(StartAfterDelay(0));
		}

	}

	IEnumerator TriggerAfterDelay(float delay, AcceptNote toTrigger, Note n)
	{
		yield return new WaitForSeconds(delay);
		toTrigger(n);
	}

	void Update()
	{
		if(!waiting)
		{
			time.addTime(song.beatsPerMeasure, Time.deltaTime, song.bpm);
			while(songNoteIndex < song.notes.Count &&
				time > song.notes[songNoteIndex].timing)
			{
				foreach(KeyValuePair<float, AcceptNote> entry in loadCursors)
				{
					StartCoroutine(TriggerAfterDelay((totalOffset + entry.Key) / 1000.0f, 
													entry.Value, 
													song.notes[songNoteIndex]));
				}
				++songNoteIndex;
			}
		}
	}
}
