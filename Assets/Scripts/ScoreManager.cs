using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance = null;



	public System.Action OnScoreChange;
	private int score_internal = 0;
	public int score
	{
		get
		{
			return score_internal;
		}
		set
		{
			if(score_internal != value)
			{
				score_internal = value;
				if(OnScoreChange != null)
					OnScoreChange();
			}
		}
	}
	public System.Action OnComboChange;
	private int combo_internal = 0;
	public int combo
	{
		get
		{
			return combo_internal;
		}
		set
		{
			if(combo_internal != value)
			{
				combo_internal = value;
				if(OnComboChange != null)
					OnComboChange();
			}
		}
	}

	public float totalAccuracy = 0.0f;
	public int notesHit = 0;

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
}
