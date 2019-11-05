using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string exitScene;
    public GameObject menu;
    public Text text;

    void Start()
    {
        PauseManager.OnEnter[PauseManager.State.Paused] += ShowMenu;
        PauseManager.OnExit[PauseManager.State.Paused] += HideMenu;

        PauseManager.OnEnter[PauseManager.State.Dead] += ShowMenu;
        PauseManager.OnExit[PauseManager.State.Dead] += HideMenu;
    }

    void OnDestroy()
    {
        PauseManager.OnEnter[PauseManager.State.Paused] -= ShowMenu;
        PauseManager.OnExit[PauseManager.State.Paused] -= HideMenu;
    
        PauseManager.OnEnter[PauseManager.State.Dead] -= ShowMenu;
        PauseManager.OnExit[PauseManager.State.Dead] -= HideMenu;
    }

    public void ShowMenu()
    {
        switch(PauseManager.currentState)
        {
            case PauseManager.State.Dead:
                text.text = "FAILURE";
                break;
            case PauseManager.State.Paused:
                text.text = "PAUSED";
                break;
        }
        menu.SetActive(true);
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        SceneManager.LoadScene(exitScene);
    }
}
