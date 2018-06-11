using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject _pausedMenuPanel;
    [SerializeField]
    private GameObject _settingsPanel;
    [SerializeField]
    private bool _gamePaused = false;
    [SerializeField]
    private MonoBehaviour[] _scriptsToDisableOnPause;

    private void Awake()
    {
        _pausedMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("PauseGame"))
        {
            ToggleGamePaused();
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ToggleGamePaused()
    {
        _gamePaused = !_gamePaused;

        if (_gamePaused)
        {
            _pausedMenuPanel.SetActive(true);
            Time.timeScale = 0;

            DisableScripts();
        }
        else
        {
            _pausedMenuPanel.SetActive(false);
            _settingsPanel.SetActive(false);
            Time.timeScale = 1f;

            EnableScripts();
        }
    }

    public void ToggleSettingsPanel()
    {
        _settingsPanel.SetActive(true);
    }

    private void EnableScripts()
    {
        foreach (MonoBehaviour script in _scriptsToDisableOnPause)
            script.enabled = true;
    }

    private void DisableScripts()
    {
        foreach (MonoBehaviour script in _scriptsToDisableOnPause)
            script.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
