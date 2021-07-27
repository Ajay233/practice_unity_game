using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayerSetupCanvas : MonoBehaviour
{
    private Transform _playButton;
    public void Start()
    {
        _playButton = transform.GetChild(0);
        if (_playButton == null)
        {
            Debug.LogError("Unable to find playButton");
        }
    }

    

    public void GoToNextScene()
    {
        SceneManager.LoadScene(2);
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
