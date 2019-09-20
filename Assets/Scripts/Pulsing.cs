using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsing : MonoBehaviour
{
    public float rate = 1.0f;
    public float magnitude = 0.5f;

    // Update is called once per frame
    void Update()
    {
        float state = 1 + magnitude * Mathf.Sin(Time.time * rate);
        transform.localScale = new Vector2(state, state);
    }
}
