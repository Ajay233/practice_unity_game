using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayerSetupCanvas : MonoBehaviour
{
    private Transform _playButton;
    private GameManager _gameManager;
    private SinglePlayerSetupCanvas _singlePlayerSetupCanvas;
    private GameObject _gameOverPanel;

    //private void Awake()
    //{
    //    if (_singlePlayerSetupCanvas == null)
    //    {
    //        _singlePlayerSetupCanvas = this;
    //        DontDestroyOnLoad(gameObject);
    //    } else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public void Start()
    {
        _playButton = transform.GetChild(0);
        if (_playButton == null)
        {
            Debug.LogError("Unable to find playButton");
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Unable to find GameManager");
        }

        _gameOverPanel = transform.GetChild(8).gameObject;
        if (_gameOverPanel == null)
        {
            Debug.LogError("Unable to find the Game Over panel");
        }
    }

    

    public void GoToNextScene()
    {
        _gameManager.ToggleStartGame();
        _gameManager.ToggleTurn(true);
        transform.GetChild(3).gameObject.SetActive(false);
        //transform.GetComponent<AudioSource>().Play();
        HidePlayButton();
    }

    public void DisplayPlayButton()
    {
        _playButton.gameObject.SetActive(true);
    }

    public void HidePlayButton()
    {
        _playButton.gameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        _gameManager.ResetGame();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
        _gameManager.ResetGame();
    }

    public void ShowGameOverButtons()
    {
        transform.GetChild(6).gameObject.SetActive(true);
        transform.GetChild(7).gameObject.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        if (_gameManager.PlayerHasWon())
        {
            _gameOverPanel.transform.GetChild(0).gameObject.SetActive(true);
        } else
        {
            _gameOverPanel.transform.GetChild(1).gameObject.SetActive(true);
        }
        _gameOverPanel.SetActive(true);
    }

}
