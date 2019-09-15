using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    public int beatsPerMeasure = 4;
    public SoundType toPlay;

    int lastBeat = 0;
    void Update()
    {
        int currentBeat = (FlowManager.instance.time.bars * beatsPerMeasure) + FlowManager.instance.time.beats;
        if(currentBeat > lastBeat)
        {
            StartCoroutine(Delay(currentBeat % beatsPerMeasure == 0));
            lastBeat = currentBeat;
        }
    }

    IEnumerator Delay(bool loud)
    {
        yield return new WaitForSeconds(FlowManager.instance.actualOffset/-1000f);
        HitsoundManager.instance.PlaySound(toPlay, loud);
    }
}
