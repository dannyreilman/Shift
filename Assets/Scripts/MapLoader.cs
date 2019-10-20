using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class MapLoader : MonoBehaviour
{
    public static MapLoader instance = null;
    public string gameplayScene;
    public LoadedSong loaded = null;
    
    [System.Serializable]
    public struct DifficultyIndex
    {
        public string name;
        public string displayedName;
        public float stars;
    }

    [System.Serializable]
    struct MapIndex
    {
        public string name;
        public string songfile;
        public float bpm;
        public float offset;
        public int beatsPerMeasure;
        public DifficultyIndex[] difficulties;
    }

    class MapWrapper
    {
        public MapIndex index;
        public string directory;
    }

    List<MapWrapper> availableMaps;

    public int GetMapCount()
    {
        return availableMaps.Count;
    }

    public string GetSongName(int index)
    {
        return availableMaps[index].index.name;
    }

    public DifficultyIndex[] GetDifficulties(int index)
    {
        return availableMaps[index].index.difficulties;
    }

    void Awake()
    {
        if(instance == null || instance.Equals(null))
        {
            instance = this;
            string[] directories = System.IO.Directory.GetDirectories("Songs");
            availableMaps = new List<MapWrapper>();
            foreach(string directory in directories)
            {
                if(System.IO.File.Exists(directory + System.IO.Path.DirectorySeparatorChar + "Index.txt"))
                {
                    MapWrapper toAdd = new MapWrapper();
                    toAdd.directory = Path.GetFullPath(directory);
                    toAdd.index = JsonUtility.FromJson<MapIndex>(System.IO.File.ReadAllText(directory + System.IO.Path.DirectorySeparatorChar + "Index.txt"));
                    availableMaps.Add(toAdd);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void LoadMap(int indexToLoad, int difficultyIndex)
    {
        if(indexToLoad >= availableMaps.Count)
        {
            throw new ArgumentException("Invalid map index");
        }
        string toLoadFrom = availableMaps[indexToLoad].directory + System.IO.Path.DirectorySeparatorChar + availableMaps[indexToLoad].index.difficulties[difficultyIndex].name + ".txt";
        string loadedString = System.IO.File.ReadAllText(toLoadFrom);
        if(loaded != null)
            ClearLoaded();
        
        loaded = (LoadedSong)ScriptableObject.CreateInstance("LoadedSong");
        loaded.beatsPerMeasure = availableMaps[indexToLoad].index.beatsPerMeasure;
        loaded.bpm = availableMaps[indexToLoad].index.bpm;
        loaded.name = availableMaps[indexToLoad].index.name;
        loaded.offset = availableMaps[indexToLoad].index.offset;
        loaded = LoadSongFromFile.LoadSong(loadedString, loaded);
        SongfileImporter.instance.LoadSong(availableMaps[indexToLoad].directory + System.IO.Path.DirectorySeparatorChar + availableMaps[indexToLoad].index.songfile);
        loaded.SongIsLoading();

        Debug.Log("Successful Load Map");
    }

    public void LoadGameplayScene()
    {
        SceneManager.LoadScene(gameplayScene);
    }

    public void ClearLoaded()
    {
        if(loaded != null && !loaded.Equals(null))
            Destroy(loaded);
        loaded = null;
    }
}
