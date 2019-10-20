using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnKeypress : MonoBehaviour
{
    public string keypress;
    public string toLoad;

    void Update()
    {
        if(Input.GetKeyDown(keypress))
        {
            LoadScene();
        }
    }

    public void LoadScene()
    {   
        SceneManager.LoadScene(toLoad);
    }
}
