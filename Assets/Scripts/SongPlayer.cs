using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongPlayer : MonoBehaviour
{
	private AudioSource source;

	public void Play(LoadedSong song, float delay)
	{
		source.clip = song.song;
		if(delay < 0)
		{
			throw new System.ArgumentException("Delay cannot be negative", "delay");
		}
		else
		{
			StartCoroutine(PlayAfterDelay(delay));
		}
	}
	IEnumerator PlayAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		source.Play();
	}
	void Awake()
	{
		source = GetComponent<AudioSource>();
	}

}
