using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject _howToPlayPanel;
    [SerializeField]
    private GameObject _loadingLevelPanel;
    [SerializeField]
    private Slider _loadingProgressSlider;

    private AsyncOperation _asyncLevel = null;
    private bool _howToPlayEnabled = false;

    public void StartGame()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void ToggleHowtoPlay()
    {
        _howToPlayEnabled = !_howToPlayEnabled;

        if (_howToPlayEnabled)
        {
            _howToPlayPanel.SetActive(true);
        }
        else
        {
            _howToPlayPanel.SetActive(false);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        EnableLoadingPanel();
        _asyncLevel = SceneManager.LoadSceneAsync(levelIndex);

        while (!_asyncLevel.isDone)
        {
            UpateLoadingProgress(_asyncLevel.progress);
            yield return null;
        }

        Time.timeScale = 1f;
        DisableLoadingPanel();
    }

    private void EnableLoadingPanel()
    {
        _loadingLevelPanel.SetActive(true);
    }

    private void DisableLoadingPanel()
    {
        _loadingLevelPanel.SetActive(false);
    }

    private void UpateLoadingProgress(float progress)
    {
        Debug.Log("Level is loading");
        _loadingProgressSlider.value = progress;
    }
}
