using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ComboApplause : MonoBehaviour
{
    AudioSource source;

    public int triggerStage;
    public float minPitch;
    public float maxPitch;

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
        if(ScoreManager.instance.GetComboStage() >= triggerStage)
        {
            source.pitch = Random.Range(minPitch, maxPitch);
            source.Play();
        }
    }
}
