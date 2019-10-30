using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitsoundManager : MonoBehaviour
{
    public static HitsoundManager instance = null;
    static float SPAM_VOLUME = 0.25f;
	public AudioSource hitsoundSource;
	public AudioSource missSource;

    void Awake()
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

    void Start()
    {
        ScoreManager.instance.OnComboChange += ComboChange;
    }

    void OnDestroy()
    {
        ScoreManager.instance.OnComboChange -= ComboChange;
    }

	public AudioClip normalSound;
	public AudioClip comboBreakSound;

	public void PlaySound(SoundType type, bool noteHit = true)
	{
		if(type == SoundType.Normal)
		{
			hitsoundSource.PlayOneShot(normalSound, (noteHit?1.0f:SPAM_VOLUME));
		}
	}

    public void ComboChange()
    {
        if(ScoreManager.instance.combo == 0)
        {
            missSource.PlayOneShot(comboBreakSound, 1.0f);
        }
    }
}
