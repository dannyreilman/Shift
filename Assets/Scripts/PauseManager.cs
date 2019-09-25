using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance = null;
    public delegate void Callable();
    public static Callable OnPause;
    public static Callable OnUnpause;
    public static Callable PausableUpdate;

    static bool paused_internal = false;
    public static bool paused
    {
        get
        {
            return paused_internal;
        }
        set
        {
            if(paused_internal != value)
            {
                if(value)
                {
                    if(OnPause != null)
                        OnPause();
                    Debug.Log("Pause"); 
                }
                else
                {
                    if(OnUnpause != null)
                        OnUnpause();
                    Debug.Log("Unpause");
                }
                paused_internal = value;
                AudioListener.pause = paused_internal;
            }
        }
    }

    void Awake()
    {
        if(instance == null || instance.Equals(null))
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        OnPause += StopTime;
        OnUnpause += StartTime;
        KeybindManager.acceptAlways[KeybindManager.InputAction.pause] += Pause;
    }

    void OnDestroy()
    {
        //Auto unpause when leaving scene with a pause screen
        paused = false;
        OnPause -= StopTime;
        OnUnpause -= StartTime;
        KeybindManager.acceptAlways[KeybindManager.InputAction.pause] -= Pause;
    }

    public void Pause()
    {
        paused = !paused;
    }

    void StopTime()
    {
        Time.timeScale = 0.0f;
    }

    void StartTime()
    {
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        if(!paused && PausableUpdate != null)
        {
            PausableUpdate();
        }
    }
}
