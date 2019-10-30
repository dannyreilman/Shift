using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HitNameDisplay : MonoBehaviour
{
	public float fadeDelay = 1.0f;
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
		ScoreManager.instance.OnScoreChange += Pop;
		ScoreManager.instance.HitName += ChangeName;
	}

	void OnDestroy()
	{
		ScoreManager.instance.OnScoreChange -= Pop;
		ScoreManager.instance.HitName -= ChangeName;
	}

	void Pop()
	{
		if(attachedAnimator != null)
			attachedAnimator.SetTrigger("Pop");
	}

	float timePassed = 0;
	void ChangeName(string name, Color color)
	{
		attachedText.text = name;
		attachedText.color = color;
		timePassed = 0;
	}


	
	// Update is called once per frame
	void Update ()
	{
		timePassed += Time.deltaTime;
		if(timePassed > fadeDelay)
		{
			attachedText.text = "";
		}
	}
}
