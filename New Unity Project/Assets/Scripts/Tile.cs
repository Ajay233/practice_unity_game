using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tile : MonoBehaviour
{
    private GameManager _gameManager;
    private SinglePlayerSetupCanvas _singlePlayerSetupCanvas;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Unable to find Game Manager");
        }
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            _singlePlayerSetupCanvas = GameObject.Find("SinglePlayerSetupCanvas").GetComponent<SinglePlayerSetupCanvas>();
            if (_singlePlayerSetupCanvas == null)
            {
                Debug.LogError("Unable to find the SinglePlayerSetupCanvas");
            }
        }
    }
    // This will need to be used only in scene 1
    void OnMouseDown()
    {
        Debug.Log("Well well well");
        SpawnShips();
        ShowOrHidePlayButton();
    }

    void SpawnShips()
    {
        if (transform.childCount == 0)
        {
            _gameManager.SpawnShip(this);
        }
        else
        {
            Transform ship = transform.GetChild(0);
            Destroy(ship.gameObject);
            _gameManager.DecreasePlayerShipCount();
        }
    }

    void ShowOrHidePlayButton()
    {
        if (_gameManager.PlayerShipCountAtMax())
        {
            _singlePlayerSetupCanvas.DisplayPlayButton();
        } else
        {
            _singlePlayerSetupCanvas.HidePlayButton();
        }
    }
}
