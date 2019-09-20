using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    public static MapLoader instance = null;
    public string gameplayScene;
    public LoadedSong loaded = null;

    void Awake()
    {
        if(instance == null || instance.Equals(null))
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void LoadMap(string mapToLoad)
    {
        TextAsset songFile = Resources.Load<TextAsset>("SongFiles/" + mapToLoad);
        loaded = LoadSongFromFile.LoadSong(songFile);
        SceneManager.LoadScene(gameplayScene, LoadSceneMode.Single);
    }
}
