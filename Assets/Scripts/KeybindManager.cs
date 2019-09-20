using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class KeybindManager : MonoBehaviour {
	public delegate void AcceptInput();
	public static Dictionary<InputAction, AcceptInput> accept;

	public static KeybindManager instance = null;

	public enum InputAction
	{
		upHit0 = 0,
		midHit0,
		downHit0,
		upHit1 = 10,
		midHit1,
		downHit1,
		upHit2 = 20,
		midHit2,
		downHit2,
		upHit3 = 30,
		midHit3,
		downHit3,
		increaseSpeed,
		decreaseSpeed	
	}

	public static char BINDING_DELIM = ':';

	public static InputAction GetRowHit(int row, NoteType type)
	{
		switch(type)
		{
			case NoteType.DownHit:
				switch(row)
				{
					case 0:
						return InputAction.downHit0;
					case 1:
						return InputAction.downHit1;
					case 2:
						return InputAction.downHit2;
					case 3:
						return InputAction.downHit3;
				}
				break;
			case NoteType.Hit:
				switch(row)
				{
					case 0:
						return InputAction.midHit0;
					case 1:
						return InputAction.midHit1;
					case 2:
						return InputAction.midHit2;
					case 3:
						return InputAction.midHit3;
				}
				break;
			case NoteType.UpHit:
				switch(row)
				{
					case 0:
						return InputAction.upHit0;
					case 1:
						return InputAction.upHit1;
					case 2:
						return InputAction.upHit2;
					case 3:
						return InputAction.upHit3;
				}
				break;
		}
		return InputAction.downHit0;
	}

	[System.Serializable]
	public struct Binding
	{
		public KeyCode key;
		public InputAction action;
	}

	public TextAsset bindingsFile;
	List<Binding> bindings;


	void LoadBindingsFromFile(TextAsset file)
	{
		StringReader reader = new StringReader(file.text);
		bindings = new List<Binding>();
        while(reader.Peek() > -1)
        {
			string line = reader.ReadLine();
			try
			{
				string[] parts = line.Split(BINDING_DELIM);
				Binding toAdd;
				toAdd.action = (InputAction)System.Enum.Parse(typeof(InputAction), parts[0].Trim(' '), true);
				toAdd.key = (KeyCode)System.Enum.Parse(typeof(KeyCode), parts[1].Trim(' '), true);
				bindings.Add(toAdd);
			}
			catch(System.Exception e)
			{
				Debug.Log("Failed to parse binding\n" + line);
			}
		}
	}

	public static bool GetDown(InputAction action)
	{
		for(int i = 0; i < instance.bindings.Count; ++i)
		{
			if(instance.bindings[i].action == action &&
			   Input.GetKey(instance.bindings[i].key))
				return true;
		}
		return false;
	}

	// Use this for initialization
	void Awake ()
	{
		if(instance == null || instance.Equals(null))
		{
			instance = this;
			accept = new Dictionary<InputAction, AcceptInput>();
			foreach(InputAction action in System.Enum.GetValues(typeof(InputAction)))
			{
				accept.Add(action, null);
			}
			LoadBindingsFromFile(bindingsFile);
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
			if(Input.GetKeyDown(bindings[i].key))
			{
				if(accept[bindings[i].action] != null)
					accept[bindings[i].action]();
			}
		}
	}
}
