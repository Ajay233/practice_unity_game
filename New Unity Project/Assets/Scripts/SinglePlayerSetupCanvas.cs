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
        //SceneManager.LoadScene(2);  //<-- This may no longer be needed if we just use the one scene?
        _gameManager.StartGame();
        transform.GetComponent<AudioSource>().Play();
        // If we use the same scene, we can add a line here to hide the button again
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
