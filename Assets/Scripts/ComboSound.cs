using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ComboSound : MonoBehaviour
{
    public int maxCombo;
    public float minPitch;
    public float maxPitch;

    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.instance.OnComboStageChange += OnComboChange;
    }
    
    void OnDestroy()
    {
        ScoreManager.instance.OnComboStageChange -= OnComboChange;
    }
    
    public void OnComboChange()
    {
        int effectiveCombo = Mathf.Min(ScoreManager.instance.GetComboStage(), maxCombo);

        if(effectiveCombo != 0)
        {
            float pitch = minPitch + ((maxPitch - minPitch) * (float)effectiveCombo / (float)maxCombo);
            source.pitch = pitch;
            source.Play();
        }
    }
}
