using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Unable to find Game Manager");
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Well well well");
        // Add condition to check if there's already a ship.  If yes, destroy the ship object
        // else
        // Add condition to only add 5 for player if we're on scene 1
        _gameManager.SpawnShip(this);
    }
}
