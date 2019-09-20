using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitLight : MonoBehaviour
{
    public int lane;
    public Image upImage;
    public Image midImage;
    public Image downImage;
    public float fadeSpeed;

    float upIntensity = 0.0f;
    float midIntensity = 0.0f;
    float downIntensity = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        KeybindManager.acceptUnpaused[KeybindManager.GetRowHitAction(lane, NoteType.DownHit)] += TriggerDown;
        KeybindManager.acceptUnpaused[KeybindManager.GetRowHitAction(lane, NoteType.Hit)] += TriggerMid;
        KeybindManager.acceptUnpaused[KeybindManager.GetRowHitAction(lane, NoteType.UpHit)] += TriggerUp;
    }

    void OnDestroy()
    {
        KeybindManager.acceptUnpaused[KeybindManager.GetRowHitAction(lane, NoteType.DownHit)] -= TriggerDown;
        KeybindManager.acceptUnpaused[KeybindManager.GetRowHitAction(lane, NoteType.Hit)] -= TriggerMid;
        KeybindManager.acceptUnpaused[KeybindManager.GetRowHitAction(lane, NoteType.UpHit)] -= TriggerUp;
    }

    // Update is called once per frame
    void Update()
    {
        upIntensity -= fadeSpeed * Time.deltaTime;
        if(upIntensity < 0)
            upIntensity = 0;
        upImage.color = new Color(1,1,1, upIntensity);

        midIntensity -= fadeSpeed * Time.deltaTime;
        if(midIntensity < 0)
            midIntensity = 0;
        midImage.color = new Color(1,1,1, midIntensity);

        downIntensity -= fadeSpeed * Time.deltaTime;
        if(downIntensity < 0)
            downIntensity = 0;
        downImage.color = new Color(1,1,1, downIntensity);
    }

    public void TriggerDown()
    {
        downIntensity = 1.0f;
    }
    public void TriggerMid()
    {
        midIntensity = 1.0f;
    }
    public void TriggerUp()
    {
        upIntensity = 1.0f;
    }
}
