using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreDisplay : MonoBehaviour
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
		attachedText.text = ScoreManager.instance.score.ToString();
	}
}
