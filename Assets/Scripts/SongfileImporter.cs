using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongfileImporter : MonoBehaviour
{
    [HideInInspector()]
    public AudioImporter importer;
    public static SongfileImporter instance = null;
    void Awake()
    {
        if(instance == null || instance.Equals(null))
        {
            instance = this;
            importer = GetComponent<AudioImporter>();
            importer.Loaded += Loaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    bool once = false;
    void Update()
    {
        if(importer.isError)
        {
            Debug.Log(importer.error);
            Debug.Break();
        }
    }

    public void LoadSong(string path)
    {
        if(importer.audioClip != null)
        {
            importer.ClearClip();
        }
        importer.Import(path);
    }

    public void Loaded(AudioClip toReceive)
    {
        Debug.Log("Loaded");
    }
}
