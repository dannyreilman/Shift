using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SongSelect))]
public class SongSelectTest : SongSelectController
{
    bool direction = true;
    float speed = 10.0f;
    float timeToChange = 5.0f;
    float timePassed = 0.0f;

    SongSelect toTest;

    void Awake()
    {
        toTest = GetComponent<SongSelect>();
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        toTest.Move((direction?1:-1) * speed * Time.deltaTime);
        while(timePassed > timeToChange)
        {
            timePassed -= timeToChange;
            direction = !direction;
        }
    }
}
