using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EternalManagersParent : MonoBehaviour
{
    public static EternalManagersParent instance = null;

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
}
