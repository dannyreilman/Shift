using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public enum State
    {
        Gameplay,
        Paused,
        Dead
    }

    static State currentState_internal = State.Gameplay;

    public static State currentState
    {
        get
        {
            return currentState_internal;
        }
        set
        {
            if(currentState_internal != value)
            {
                if(OnExit[currentState_internal] != null)
                    OnExit[currentState_internal]();

                currentState_internal = value;

                if(OnEnter[currentState_internal] != null)
                    OnEnter[currentState_internal]();
            }
        }
    }
    public static PauseManager instance = null;
    public static Dictionary<State, System.Action> OnEnter = null;
    public static Dictionary<State, System.Action> OnExit = null;
    public static System.Action PausableUpdate;

    public static bool paused
    {
        get
        {
            return currentState == State.Paused;
        }
    }

    void Awake()
    {
        if(instance == null || instance.Equals(null))
        {
            instance = this;
            if(OnEnter == null || OnEnter.Equals(null))
            {
                OnEnter = new Dictionary<State, System.Action>();
                foreach(State action in System.Enum.GetValues(typeof(State)))
                {
                    OnEnter[action] = null;
                }
            }
            if(OnExit == null|| OnExit.Equals(null))
            {
                OnExit = new Dictionary<State, System.Action>();
                foreach(State action in System.Enum.GetValues(typeof(State)))
                {
                    OnExit[action] = null;
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        OnEnter[State.Paused] += StopTime;
        OnEnter[State.Gameplay] += StartTime;
        KeybindManager.acceptAlways[KeybindManager.InputAction.pause] += Pause;
    }

    void OnDestroy()
    {
        //Auto unpause when leaving scene with a pause screen
        currentState = State.Gameplay;
        OnEnter[State.Paused] -= StopTime;
        OnEnter[State.Gameplay] -= StartTime;
        KeybindManager.acceptAlways[KeybindManager.InputAction.pause] -= Pause;
    }

    public void Pause()
    {
        if(paused)
            currentState = State.Gameplay;
        else if(currentState == State.Gameplay)
            currentState = State.Paused;
    }

    void StopTime()
    {
        Time.timeScale = 0.0f;
        AudioListener.pause = true;
    }

    void StartTime()
    {
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
    }

    void Update()
    {
        if(!paused && PausableUpdate != null)
        {
            PausableUpdate();
        }
    }
}
