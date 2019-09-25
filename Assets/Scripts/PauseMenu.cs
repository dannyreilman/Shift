using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string exitScene;

    void Start()
    {
        PauseManager.OnPause += ShowMenu;
        PauseManager.OnUnpause += HideMenu;
    }

    void OnDestroy()
    {
        PauseManager.OnPause -= ShowMenu;
        PauseManager.OnUnpause -= HideMenu;
    }

    public void ShowMenu()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        transform.GetChild(0).gameObject.SetActive(false);
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
