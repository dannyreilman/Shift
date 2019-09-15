using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Animator))]
public class ComboDisplay : MonoBehaviour
{
	Text attachedText;
	Animator attachedAnimator;
	// Use this for initialization
	void Awake ()
	{
		attachedText = GetComponent<Text>();
		attachedAnimator = GetComponent<Animator>();
	}

	void Start()
	{
		ScoreManager.instance.OnScoreChange += delegate(){attachedAnimator.SetTrigger("Pop");};
	}


	
	// Update is called once per frame
	void Update ()
	{
		if(ScoreManager.instance.combo != 0)
		{
			attachedText.text = ScoreManager.instance.combo.ToString() + "x";
		}
		else
		{
			attachedText.text = "";
		}
	}
}
