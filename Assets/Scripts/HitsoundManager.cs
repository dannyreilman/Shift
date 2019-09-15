using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HitsoundManager : MonoBehaviour
{
    public static HitsoundManager instance = null;
    static float SPAM_VOLUME = 0.25f;
	AudioSource attachedSource;

    void Awake()
    {
        if(instance == null || instance.Equals(null))
        {
            instance = this;
			attachedSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(this);
        }
    }

	public AudioClip normalSound;

	public void PlaySound(SoundType type, bool noteHit = true)
	{
		if(type == SoundType.Normal)
		{
			attachedSource.PlayOneShot(normalSound, (noteHit?1.0f:SPAM_VOLUME));
		}

	}
}
