using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnSpace : MonoBehaviour
{
    public string toLoad;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            MapLoader.instance.LoadMap(toLoad);
            Destroy(gameObject);
        }
    }
}
