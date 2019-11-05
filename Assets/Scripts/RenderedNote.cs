using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class RenderedNote : MonoBehaviour
{
	public Sprite upHitSprite;
	public Sprite normalSprite;
	public Sprite downHitSprite;

	Image attachedImage;

	void Awake()
	{
		attachedImage = GetComponent<Image>();
	}

	void Start()
	{
		PauseManager.OnExit[PauseManager.State.Gameplay] += HideNote;
		PauseManager.OnEnter[PauseManager.State.Gameplay] += ShowNote;
	}

	void OnDestroy()
	{
		PauseManager.OnExit[PauseManager.State.Gameplay] -= HideNote;
		PauseManager.OnEnter[PauseManager.State.Gameplay] -= ShowNote;
	}

	public void HideNote()
	{
		attachedImage.enabled = false;
	}

	public void ShowNote()
	{
		attachedImage.enabled = true;
	}

	public void CleanupNote()
	{
		Destroy(gameObject);
	}

	//Some particle effect or something here
	public void HitNote()
	{
		Destroy(gameObject);
	}

	public void InitializeNote(Note n)
	{
		switch(n.type)
		{
			case NoteType.DownHit:
				attachedImage.sprite = downHitSprite;
				break;
			case NoteType.UpHit:
				attachedImage.sprite = upHitSprite;
				break;
			case NoteType.Hit:
				attachedImage.sprite = normalSprite;
				break;
		}
	}
}
