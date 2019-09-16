using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitLight : MonoBehaviour
{
    public int lane;
    public Image upLaneImage;
    public Image normalLaneImage;
    public Image downLaneImage;
    public float fadeSpeed;

    float upIntensity = 0.0f;
    float normalIntensity = 0.0f;
    float downIntensity = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        KeybindManager.accept += TriggerLight;
    }

    // Update is called once per frame
    void Update()
    {
        upIntensity -= fadeSpeed * Time.deltaTime;
        if(upIntensity < 0)
            upIntensity = 0;
        upLaneImage.color = new Color(1,1,1, upIntensity);

        normalIntensity -= fadeSpeed * Time.deltaTime;
        if(normalIntensity < 0)
            normalIntensity = 0;
        normalLaneImage.color = new Color(1,1,1, normalIntensity);

        downIntensity -= fadeSpeed * Time.deltaTime;
        if(downIntensity < 0)
            downIntensity = 0;
        downLaneImage.color = new Color(1,1,1, downIntensity);
    }

    public void TriggerLight(int row, NoteType type)
    {
        if(row == lane)
        {
            switch(type)
            {
                case NoteType.DownHit:
                    downIntensity = 1.0f;
                    break;
                case NoteType.Hit:
                    normalIntensity = 1.0f;
                    break;
                case NoteType.UpHit:
                    upIntensity = 1.0f;
                    break;
            }
        }
    }
}
