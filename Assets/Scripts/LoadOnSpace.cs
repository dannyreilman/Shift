using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnSpace : MonoBehaviour
{
    public int toLoad;
    public int difficultyToLoad;

    void Start()
    {
        MapLoader.instance.LoadMap(toLoad, difficultyToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            MapLoader.instance.LoadGameplayScene();
            Destroy(gameObject);
        }
    }
}
