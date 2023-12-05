using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool GameIsPaused = false;

    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _pauseBackground;

    public void Pause()
    {
        if (GameIsPaused == false)
        {
            _pauseMenuUI.SetActive(true);
            _pauseBackground.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        else
        {
            _pauseMenuUI.SetActive(false);
            _pauseBackground.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
    }
}
