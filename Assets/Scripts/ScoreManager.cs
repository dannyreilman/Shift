using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance = null;

	public int[] comboStages;
	public int comboBase;
	public float stageScale = 2f;

	public int GetComboStage()
	{
		return stageIndex;
	}

	int stageIndex = 0;

	public int GetComboScale()
	{
		return comboStages[stageIndex];
	}
	

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
	public System.Action OnComboStageChange;
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

				if(combo_internal == 0)
				{
					stageIndex = 0;
					if(OnComboStageChange != null)
						OnComboStageChange();
				}
				else if(stageIndex + 1 < comboStages.Length && combo_internal > GetRequiredCombo())
				{
					++stageIndex;
					Debug.Log(stageIndex);
					if(OnComboStageChange != null)
						OnComboStageChange();
				}
			}
		}
	}

	// Returns the required combo to advance to the next stage
	// This is equal to comboBase * scale ^n summed with n from 0 to index
	// This summation is also equal to B((S^(index + 1) - 1)/(S-1))
	int GetRequiredCombo()
	{
		return Mathf.CeilToInt(comboBase * (Mathf.Pow(stageScale, stageIndex + 1) - 1) / (stageScale - 1));
	}

	public System.Action<string, Color> HitName;

	public float totalAccuracy = 0.0f;
	public int notesHit = 0;

	public void ResetScore()
	{
		totalAccuracy = 0.0f;
		notesHit = 0;
		combo = 0;
		score = 0;
	}

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
