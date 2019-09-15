using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Note 
{
	public Timestamp timing;
	public NoteType type;
	public int lane;
	public SoundType soundType;
	//Possibly null, the associated gameObject
	public RenderedNote rendered = null;
}
