﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	void Start()
	{
		KeybindManager.accept += AcceptInput;
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
		ClosebyNote closestNote = null;
		foreach(ClosebyNote note in notesInWindow)
		{
			if(note.note.lane == row && 
			   note.note.type == nType &&
			  (closestNote == null || Mathf.Abs(note.timing) < Mathf.Abs(closestNote.timing)))
			{
				closestNote = note;
			}
		}
		if(closestNote == null)
		{
			HitsoundManager.instance.PlaySound(SoundType.Normal, false);
			return;
		}
		ScoreManager.instance.totalAccuracy += closestNote.timing;
		ScoreManager.instance.notesHit += 1;
		Window last = timingWindows[0];
		for(int i = 1; i < timingWindows.Count; ++i)
		{
			if(Mathf.Abs(closestNote.timing) > Mathf.Abs(timingWindows[i].msDelay))
			{
				TriggerWindow(last);
				if(closestNote.note.rendered != null)
					closestNote.note.rendered.HitNote();
				HitsoundManager.instance.PlaySound(closestNote.note.soundType);
				notesInWindow.Remove(closestNote);
				return;

			}
			last = timingWindows[i];
		}
		TriggerWindow(last);
		if(closestNote.note.rendered != null)
			closestNote.note.rendered.HitNote();
		HitsoundManager.instance.PlaySound(closestNote.note.soundType);
		notesInWindow.Remove(closestNote);
		return;
	}
	
	private void TriggerWindow(Window w)
	{
		ScoreManager.instance.score += w.pointValue;
		if(w.breaksCombo)
		{
			ScoreManager.instance.combo = 0;
		}
		else
		{
			ScoreManager.instance.combo++;
		}

	}

	void Update()
	{
		for(int i = notesInWindow.Count - 1; i >= 0; --i)
		{
			notesInWindow[i].timing += Time.deltaTime * 1000.0f;
			if(notesInWindow[i].timing > timingWindows[0].msDelay)
			{
				if(notesInWindow[i].note.rendered != null)
					notesInWindow[i].note.rendered.CleanupNote();
				ScoreManager.instance.combo = 0;
				notesInWindow.RemoveAt(i);
				//Debug.Log("Removing...");
			}
		}
	}
}