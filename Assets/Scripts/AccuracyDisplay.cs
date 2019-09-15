using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AccuracyDisplay : MonoBehaviour
{
	Text attachedText;
	// Use this for initialization
	void Awake ()
	{
		attachedText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(ScoreManager.instance.notesHit != 0)
		{
			attachedText.text = "Accuracy:" + (ScoreManager.instance.totalAccuracy/ ScoreManager.instance.notesHit).ToString();
		}
		else
		{
			attachedText.text = "";
		}
	}
}
