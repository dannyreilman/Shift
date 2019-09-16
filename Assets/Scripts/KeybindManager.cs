using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindManager : MonoBehaviour {
	public delegate void AcceptHit(int row, NoteType type);
	public static AcceptHit accept;

	public static KeybindManager instance = null;

	[System.Serializable]
	public struct Binding
	{
		public KeyCode up;
		public KeyCode mid;
		public KeyCode down;
	}
	public List<Binding> bindings;

	public static bool GetDown(int row, NoteType type)
	{
		switch(type)
		{
			case NoteType.DownHit:
				return Input.GetKey(instance.bindings[row].down);
			case NoteType.Hit:
				return Input.GetKey(instance.bindings[row].mid);
			case NoteType.UpHit:
				return Input.GetKey(instance.bindings[row].up);
			default:
				return false;
		}
	}

	// Use this for initialization
	void Awake ()
	{
		if(instance == null || instance.Equals(null))
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	void Update()
	{
		for(int i = 0; i < bindings.Count; ++i)
		{
			if(Input.GetKeyDown(bindings[i].up))
			{
				accept(i, NoteType.UpHit);
			}
			else if(Input.GetKeyDown(bindings[i].mid))
			{
				accept(i, NoteType.Hit);
			}
			else if(Input.GetKeyDown(bindings[i].down))
			{
				accept(i, NoteType.DownHit);
			}
		}
	}
}
