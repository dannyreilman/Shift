using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SongSelect))]
public class SongSelectArrows : SongSelectController
{
    const float DEADZONE = 0.1f;
    public float speed;
    public float[] delays;
    public float delayChangeTime;
    int delayIndex = 0;
    float heldTime = 0;
    float totalHeldTime = 0;

    int goalChange = 0;

    SongSelect songSelect;
    void Awake()
    {
        songSelect = GetComponent<SongSelect>();
    }

    void Start()
    {
        songSelect.ActiveSongChanged += ActiveSongChanged;
    }

    void OnDestroy()
    {
        songSelect.ActiveSongChanged -= ActiveSongChanged;
    }

    void ActiveSongChanged(int newIndex, bool forwards)
    {
        if(goalChange != 0)
            goalChange += (forwards)?-1:1;
    }

    bool horizPressed = false;
    bool vertPressed = false;

    // Start is called before the first frame update
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if(horizontal > DEADZONE)
        {
            if(!horizPressed)
                goalChange += 1;
            horizPressed = true;
        }
        else if(horizontal < -1 * DEADZONE)
        {
            if(!horizPressed)
                goalChange -= 1;
            horizPressed = true;
        }
        else
        {
            horizPressed = false;
        }

        float vertical = Input.GetAxis("Vertical");
        if(vertical > DEADZONE)
        {
            if(!vertPressed)
                goalChange += 1;
            vertPressed = true;
        }
        else if(vertical < -1 * DEADZONE)
        {
            if(!vertPressed)
                goalChange -= 1;
            vertPressed = true;
        }
        else
        {
            vertPressed = false;
        }

        if(horizPressed == false && vertPressed == false)
        {
            delayIndex = 0;
            heldTime = 0.0f;
            totalHeldTime = 0.0f;
        }
        else
        {
            heldTime += Time.deltaTime;
            totalHeldTime += Time.deltaTime;
            if(heldTime > delays[delayIndex])
            {
                horizPressed = false;
                vertPressed = false;
                
                heldTime = 0;
            }

            if(totalHeldTime > delayChangeTime && delayIndex < delays.Length - 1)
            {
                totalHeldTime = 0;
                ++delayIndex;
            }
        }

        if(goalChange != 0)
        {
            songSelect.Move(goalChange * speed * Time.deltaTime);
        }
    }
}
