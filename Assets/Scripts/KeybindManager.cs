using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindManager : MonoBehaviour {
	public delegate void AcceptHit(int row, NoteType type);
	public static AcceptHit accept;

	static KeybindManager instance = null;

	[System.Serializable]
	public struct Binding
	{
		public KeyCode up;
		public KeyCode mid;
		public KeyCode down;
	}
	public List<Binding> bindings;

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
