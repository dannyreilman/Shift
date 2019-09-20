using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsing : MonoBehaviour
{
    public float rate = 1.0f;
    public float magnitude = 0.5f;

    void Start()
    {
        PauseManager.PausableUpdate += PausableUpdate;   
    }

    void OnDestroy()
    {
        PauseManager.PausableUpdate -= PausableUpdate; 
    }

    // Update is called once per frame
    void PausableUpdate()
    {
        float state = 1 + magnitude * Mathf.Sin(Time.time * rate);
        transform.localScale = new Vector2(state, state);
    }
}
