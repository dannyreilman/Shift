using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    public static TimeKeeper instance = null;
    public float currentTime
    {
        get
        {
            return Time.time - startTime;
        }
    }

    float bpm;
    public void SetBPM(float bpm)
    {
        this.bpm = bpm;
    }

    public Timestamp currentStamp
    {
        get
        {
            if(currentStamp_internal == null)
            {
                currentStamp_internal = new Timestamp(currentTime, 4, bpm);
            }
            return currentStamp_internal;
        }
    }

    private float startTime;
    private Timestamp currentStamp_internal = null;

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
    void ZeroTime()
    {
        startTime = Time.time;
    }

    void LateUpdate()
    {
        currentStamp_internal = null;
    }
}