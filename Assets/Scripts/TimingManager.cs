using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour, CursorUser
{

	[System.Serializable]
	public class Window
	{
		public float msDelay;
		public int pointValue;
		public bool breaksCombo;
	};

	//Windows in ms
	public List<Window> timingWindows;

	private class ClosebyNote
	{
		public float timing;
		public Note note;
	}
	//Timing to Note
	private List<ClosebyNote> notesInWindow = new List<ClosebyNote>();

	private AudioSource metronome;
	
	void Start()
	{
		KeybindManager.accept += AcceptInput;
		metronome = GetComponent<AudioSource>();
	}
	public FlowManager.AcceptNote GetCursor()
	{
		return AcceptNote;
	}

    public float GetMillisecondDelay()
    {
        return -1 * timingWindows[0].msDelay;
    }

	public void AcceptNote(Note n)
	{
		ClosebyNote toAdd = new ClosebyNote();
		toAdd.timing = -1 * timingWindows[0].msDelay;
		toAdd.note = n;
		notesInWindow.Add(toAdd);
	}

	public void AcceptInput(int row, NoteType nType)
	{
		Debug.Log("Input: " + nType);
	}

	void Update()
	{
		for(int i = notesInWindow.Count - 1; i >= 0; --i)
		{
			notesInWindow[i].timing += Time.deltaTime * 1000.0f;
			if(notesInWindow[i].timing > timingWindows[0].msDelay)
			{
				notesInWindow.RemoveAt(i);
				Debug.Log("Removing...");
			}
		}
	}
}
