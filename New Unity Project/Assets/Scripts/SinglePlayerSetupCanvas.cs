using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayerSetupCanvas : MonoBehaviour
{
    private Transform _playButton;
    private GameManager _gameManager;
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
    }

    

    public void GoToNextScene()
    {
        _gameManager.ToggleStartGame();
        _gameManager.ToggleTurn();
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
}
